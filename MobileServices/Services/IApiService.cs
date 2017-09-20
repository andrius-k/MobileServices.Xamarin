using System;
namespace MobileServices.Services
{
    public interface IApiService
    {
        IRestApi RestApi { get; }
    }
}
