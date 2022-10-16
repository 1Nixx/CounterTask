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
			Monitor.Enter(_context);

			try
			{
				checked
				{
					_context.Counter += value;
				}
			}
			finally
			{
				Monitor.Exit(_context);
			}

		}
	}
}
