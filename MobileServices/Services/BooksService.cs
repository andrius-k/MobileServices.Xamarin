using System;
using System.Collections.Generic;
using MobileServices.DTOs;
using Akavache;
using System.Threading.Tasks;
using MobileServices.Exceptions;
using System.Diagnostics;
using System.Threading;
namespace MobileServices.Services
{
    public class BooksService : IBooksService
    {
		private readonly IApiService _apiService;
		private readonly IBlobCacheService _blobCacheService;

		public BooksService(
			IApiService apiService,
			IBlobCacheService blobCacheService)
		{
			_apiService = apiService;
			_blobCacheService = blobCacheService;
		}

        public IObservable<List<Book>> GetAllBooks(CancellationToken ct = default(CancellationToken))
		{
			var cache = _blobCacheService.LocalMachine;

			var cachedItems = cache.GetAndFetchLatest("books", () => FetchRemoteBooks(ct));

			return cachedItems;
		}

		private async Task<List<Book>> FetchRemoteBooks(CancellationToken ct = default(CancellationToken))
		{
            var result = await _apiService.RestApi.GetAllBooks(ct);

			if (!result.IsSuccessStatusCode)
			{
                throw new ApiException($"Non-success status code: {result.StatusCode}");
			}

            var response = await result.Content.DeserializeTo<BooksResponse>();
            return response.Books;
		}

		public IObservable<BookDetails> GetBook(string id, CancellationToken ct = default(CancellationToken))
		{
			var cache = _blobCacheService.LocalMachine;

            var cachedItems = cache.GetAndFetchLatest($"book_{id}", () => FetchRemoteBook(id, ct));

			return cachedItems;
		}

		private async Task<BookDetails> FetchRemoteBook(string id, CancellationToken ct = default(CancellationToken))
		{
			var result = await _apiService.RestApi.GetBook(id, ct);

			if (!result.IsSuccessStatusCode)
			{
				throw new ApiException($"Non-success status code: {result.StatusCode}");
			}

            var response = await result.Content.DeserializeTo<BookDetails>();
			return response;
		}
	}
}
