using CounterTask.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CounterTask.Hubs
{
	public class CounterHub : Hub
	{
		private readonly ICounterRepository _counterRepository;
		public CounterHub(ICounterRepository counterRepository)
		{
			_counterRepository = counterRepository;	
		}
		public async Task IncreaseCounter(int value)
		{
			_counterRepository.Increase(value);

			await Clients.All.SendAsync("CounterValue", _counterRepository.Get());
		}

		public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("CounterValue", _counterRepository.Get());
		}
	}
}
