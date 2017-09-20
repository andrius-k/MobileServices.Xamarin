using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
namespace MobileServices
{
    public static class Extensions
    {
		/// <summary>
		/// Serializes HttpContent to specified type.
		/// </summary>
		public async static Task<T> DeserializeTo<T>(this HttpContent content) where T : class
		{
			T entity = await Task.Run(async () => JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync()));
			if (entity == null)
				throw new Exception("Unable to serialize entity data.");
			return entity;
		}
    }
}
