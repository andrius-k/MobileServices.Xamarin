using System.Reactive.Concurrency;
namespace MobileServices.Services
{
    public interface ISchedulerProvider
    {
		IScheduler Foreground { get; }
		IScheduler Background { get; }
    }
}
