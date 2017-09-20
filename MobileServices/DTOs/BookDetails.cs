using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
namespace MobileServices.DTOs
{
    public class BookDetails
    {
		public string Id { get; set; }
		[JsonProperty("volumeInfo")]
        public VolumeInfoDetails Info { get; set; }
    }
}
