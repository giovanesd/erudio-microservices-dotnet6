using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private const string MEDIA_TYPE = "application/json";
        private static readonly MediaTypeHeaderValue contentType = new MediaTypeHeaderValue(MEDIA_TYPE);

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode) throw
                    new ApplicationException($"Something went calling the API: " +
                    $"{responseMessage.ReasonPhrase}");
            var dataAsString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PostAsJson(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsJson(url, content);
        }
    }
}