using System.Collections.Generic;
using Newtonsoft.Json;
namespace MobileServices.DTOs
{
    public class BooksResponse
    {
        [JsonProperty("items")]
        public List<Book> Books { get; set; }
    }
}
