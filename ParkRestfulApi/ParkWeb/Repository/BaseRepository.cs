using Newtonsoft.Json;
using ParkWeb.Repository.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkWeb.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetAsync(string url, int? id)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, url + id);

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            if(objToCreate != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if(responseMessage.StatusCode == HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(string url, int id, T objToUpdate)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Patch, url + id);

            if (objToUpdate != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(objToUpdate), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string url, int id)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, url + id);

            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
