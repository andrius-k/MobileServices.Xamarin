using System.Reactive.Concurrency;
using System.Threading;
namespace MobileServices.Services
{
    public class SchedulerProvider : ISchedulerProvider
    {
		public IScheduler Background
		{
			get
			{

				return TaskPoolScheduler.Default;
			}
		}

		private IScheduler _foreground = new SynchronizationContextScheduler(SynchronizationContext.Current);
		public IScheduler Foreground
		{
			get
			{
				return _foreground;
			}
		}
    }
}
