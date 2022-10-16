using CounterTask.Interfaces;

namespace CounterTask.Data
{
	public class CounterRepository : ICounterRepository
	{
		private readonly CounterContext _context;

		public CounterRepository(CounterContext context)
		{
			_context = context;	
		}

		public int Get()
		{
			return _context.Counter;
		}

		public void Increase(int value)
		{
			checked
			{
				//Interlocked.Add(ref _context.Counter, value);
				_context.Counter += value;
			}
		}
	}
}
