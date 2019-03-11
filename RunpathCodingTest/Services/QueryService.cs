using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RunpathWebApi.Models;

namespace RunpathWebApi.Services
{
    public class QueryService : IQueryService
    {
        private static readonly HttpClient _HttpClient;

        private static readonly string BaseAddress = "http://jsonplaceholder.typicode.com/";

        private static readonly Dictionary<Type, string> TypesPrefix = new Dictionary<Type, string>
        {
            {typeof(Photo), "photos"},
            {typeof(Album), "albums"}
        };

        static QueryService()
        {
            _HttpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            _HttpClient.DefaultRequestHeaders.Accept.Clear();
            _HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        } 

        public async Task<IEnumerable<T>> GetAllAsync<T>(string query = "")
        {
            if (!string.IsNullOrEmpty(query) && !query.StartsWith("?"))
            {
                query = $"?{query}";
            }

            if (TypesPrefix.TryGetValue(typeof(T), out string prefix))
            {
                query = $"{prefix}{query}";
            }

            HttpResponseMessage responseMessage = await _HttpClient.GetAsync(query);

            if (responseMessage.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<List<T>>();
            }

            return new List<T>();
        }     
    }
}
