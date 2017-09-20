using Newtonsoft.Json;
namespace MobileServices.DTOs
{
    public class Book
    {
        public string Id { get; set; }
        [JsonProperty("volumeInfo")]
        public VolumeInfo Info { get; set; }
    }
}
