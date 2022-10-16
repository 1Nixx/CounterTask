const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/counterhub")
    .build();

hubConnection.on("CounterValue", (data) => {
    document.getElementById("serverValue").innerHTML = data;
});

hubConnection.start();

var intervalId;
const randomMinBorder = 1;
const randomMaxBorder = 1000;

document.getElementById("startBtn").addEventListener("click", () => {
    let interval = document.getElementById("inputInterval").value;

    intervalId = setInterval(SendRequest, interval);

    SendingState();
});

document.getElementById("stopBtn").addEventListener("click", () => {
    DisableReqest();

    StopState();
});

document.getElementById("inputInterval").addEventListener("input", (e) => {
    let inpValue = document.getElementById("inputInterval").value;

    if (!isNaN(inpValue) && Number.isInteger(Number(inpValue)) && inpValue > 0) 
        document.getElementById("startBtn").removeAttribute("disabled", "disabled");
    else
        document.getElementById("startBtn").setAttribute("disabled", "disabled");
})

window.addEventListener("load", () => {
    WindowLoadState();
})

function SendRequest() {
    let numb = generateRandomIntegerInRange(randomMinBorder, randomMaxBorder);
    hubConnection.invoke("IncreaseCounter", numb)
        .catch(() => {
            DisableReqest();
            ShowErrorMessage("Невозможно отправить данные на сервер");
            StopState();
        });
}

function DisableReqest() {
    clearInterval(intervalId);
}


function SendingState() {
    HideErrorMessage();
    document.getElementById("stopBtn").removeAttribute("disabled", "disabled");
    document.getElementById("startBtn").setAttribute("disabled", "disabled");
    document.getElementById("inputInterval").setAttribute("disabled", "disabled");
    document.getElementById("loading").classList.remove("d-none");
}

function StopState() {
    let inpInterval = document.getElementById("inputInterval");

    document.getElementById("stopBtn").setAttribute("disabled", "disabled");
    document.getElementById("startBtn").removeAttribute("disabled", "disabled");
    inpInterval.removeAttribute("disabled", "disabled");
    inpInterval.focus();
    document.getElementById("loading").classList.add("d-none");
}

function WindowLoadState() {
    let inpInterval = document.getElementById("inputInterval");

    inpInterval.value = '';
    inpInterval.removeAttribute("disabled", "disabled");
    inpInterval.focus();

    document.getElementById("startBtn").setAttribute("disabled", "disabled");
    document.getElementById("stopBtn").setAttribute("disabled", "disabled");

    document.getElementById("loading").classList.add("d-none");
    HideErrorMessage();
}


function ShowErrorMessage(message) {
    let errorlb = document.getElementById("lbErrorMsg");

    errorlb.innerHTML = message;
    errorlb.classList.remove("visually-hidden");
}

function HideErrorMessage() {
    let errorlb = document.getElementById("lbErrorMsg");

    errorlb.classList.add("visually-hidden");
}


function generateRandomIntegerInRange(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}