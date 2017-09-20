using Akavache;
namespace MobileServices.Services
{
    public class BlobCacheService : IBlobCacheService
    {
		public IBlobCache LocalMachine
		{
			get
			{
				return BlobCache.LocalMachine;
			}
		}
    }
}
