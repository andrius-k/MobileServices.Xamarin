using System;
using Refit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
namespace MobileServices.Services
{
    [Headers("Accept: application/json")]
    public interface IRestApi
	{
		[Get("/volumes?q=software&key=AIzaSyDN23DnwO-wFMVbGSV3uaBnYFypxou-Nd4")]
		Task<HttpResponseMessage> GetAllBooks(CancellationToken ct = default(CancellationToken));

        [Get("/volumes/{id}?key=AIzaSyDN23DnwO-wFMVbGSV3uaBnYFypxou-Nd4")]
		Task<HttpResponseMessage> GetBook(string id, CancellationToken ct = default(CancellationToken));
	}
}