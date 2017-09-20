using System;
using System.Net.Http;
using Refit;
using ModernHttpClient;

namespace MobileServices.Services
{
    public class ApiService : IApiService
    {
        public static readonly string ApiBaseAddress = "https://www.googleapis.com/books/v1";

        private readonly Lazy<IRestApi> _restApi;

		public ApiService()
		{
			_restApi = new Lazy<IRestApi>(() => 
            {
                var client = new HttpClient(new NativeMessageHandler())
				{
					BaseAddress = new Uri(ApiBaseAddress)
				};

				return RestService.For<IRestApi>(client);
            });
        }

		public IRestApi RestApi
		{
			get { return _restApi.Value; }
		}
    }
}
