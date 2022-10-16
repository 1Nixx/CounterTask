using CounterTask.Hubs;
using CounterTask.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CounterTask.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CounterController : ControllerBase
	{
		private readonly ICounterRepository _counterRepository;
		public CounterController(ICounterRepository counterRepository)
		{
			_counterRepository = counterRepository;
		}

		[HttpPost]
		public void Increase(int value, [FromServices] IHubContext<CounterHub> hubcontext)
		{
			_counterRepository.Increase(value);

			hubcontext.Clients.All.SendAsync("CounterValue", _counterRepository.Get());
		}

		[HttpGet]
		public int Get()
		{
			return _counterRepository.Get();
		}
	}
}
