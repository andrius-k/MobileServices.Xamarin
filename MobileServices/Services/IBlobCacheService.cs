using Akavache;
namespace MobileServices.Services
{
    public interface IBlobCacheService
    {
        IBlobCache LocalMachine { get; }
    }
}
