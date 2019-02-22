using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RxUIDemoApp.Models;

namespace RxUIDemoApp.Services
{
    public static class RestService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<Human> Get(long id)
        {
            var response = await HttpClient.GetAsync(new Uri("https://swapi.co/api/people/" + id + @"/"));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = JsonConvert.DeserializeObject<Human>(await response.Content.ReadAsStringAsync());
                return content;
            }
            return null;
        }
    }
}