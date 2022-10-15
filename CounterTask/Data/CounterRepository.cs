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
			throw new NotImplementedException();
		}

		public void Increase(int value)
		{
			throw new NotImplementedException();
		}
	}
}
