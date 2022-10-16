using CounterTask.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

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
		public void Increase(int value)
		{
			_counterRepository.Increase(value);
		}

		[HttpGet]
		public int Get()
		{
			return _counterRepository.Get();
		}
	}
}
