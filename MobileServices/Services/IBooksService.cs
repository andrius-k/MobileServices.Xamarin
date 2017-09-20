using System;
using System.Collections.Generic;
using MobileServices.DTOs;
using System.Threading;
namespace MobileServices.Services
{
    public interface IBooksService
    {
        IObservable<List<Book>> GetAllBooks(CancellationToken ct = default(CancellationToken));
        IObservable<BookDetails> GetBook(string id, CancellationToken ct = default(CancellationToken));
    }
}
