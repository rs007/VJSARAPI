using Newtonsoft.Json;
using System.Net.Http.Headers;
using VJSARAPI.Models;

namespace VJSARAPI.Services
{
    public class AlbumService : IAlbumService
    {
        public async Task<List<Album>> GetAlbums()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync($"/albums").Result;
            string stringJWT = response.Content.ReadAsStringAsync().Result;
            List<Album> jwt = JsonConvert.DeserializeObject<List<Album>>(stringJWT);

            return await Task.FromResult(jwt);
        }
        public async Task<List<Photo>> GetPhotos()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com");
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            HttpResponseMessage response = client.GetAsync($"/photos").Result;
            string stringJWT = response.Content.ReadAsStringAsync().Result;
            List<Photo> jwt = JsonConvert.DeserializeObject<List<Photo>>(stringJWT);
            return await Task.FromResult(jwt);
        }
    }
}
