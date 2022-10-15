namespace CounterTask.Interfaces
{
	public interface ICounterRepository
	{
		void Increase(int value);
		int Get();
	}
}
