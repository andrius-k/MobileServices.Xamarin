using System;
using Newtonsoft.Json;
namespace MobileServices.DTOs
{
    public class VolumeInfoDetails
    {
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string[] Authors { get; set; }
		[JsonProperty("averageRating")]
		public double Rating { get; set; }
        public string Description { get; set; }
        public ImageLinks ImageLinks { get; set; }
    }
}
