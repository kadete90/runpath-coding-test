using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RunpathCodingTest.Services
{
    public class QueryService : IQueryService
    {
        private static readonly HttpClient HttpClient;

        private static readonly string BaseAddress = "http://jsonplaceholder.typicode.com/";

        //Dictionary<Type, string> typesPrefix = new Dictionary<Type, string>
        //{
        //    {typeof(Photo), "photos"},
        //    {typeof(Album), "albums"}
        //};

        static QueryService()
        {
            HttpClient = new HttpClient { BaseAddress = new Uri(BaseAddress) };
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string query = null)
        {
            HttpResponseMessage responseMessage = await HttpClient.GetAsync(query);

            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadAsAsync<T[]>();
            }

            return new List<T>();
        }
    }
}
