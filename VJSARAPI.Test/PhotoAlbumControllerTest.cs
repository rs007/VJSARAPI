using Newtonsoft.Json;
using System.Net.Http.Headers;
using VJSARAPI.Controllers;
using VJSARAPI.Models;

namespace VJSARAPI.Test
{
    public class PhotoAlbumControllerTest
    {
        [Fact]
        public void GetCombineResult_Returns_Correct_NumberOfPhotoAlbum()
        {
            // Arrange
            var test = GetCombineResult(1);

            // Act
            var controller = new PhotoAlbumController();
            var result = controller.GetCombine(1);
            IEnumerable<Combine> count = result.Result.ToList();
            // Assert
            Assert.Equal(test.Result.Count(), count.Count());
        }

        private async Task<IEnumerable<Combine>> GetCombineResult(int id)
        {
            List<Album> albumslst = await GetAlbums();
            List<Photo> photolst = await GetPhotos();

            var query = from album in albumslst
                        join photo in photolst
                        on album.Id equals photo.AlbumId
                        select new Combine
                        {
                            UserId = album.UserId,
                            Id = photo.Id,
                            AlbumId = photo.AlbumId,
                            Url = photo.Url,
                            ThumbnailUrl = photo.ThumbnailUrl,
                            Title = photo.Title
                        };
            query = query.Where(f => f.UserId == id).ToList();

            IEnumerable<Combine> result = new List<Combine>();
            result = query;
            return result;
        }

        private async Task<List<Album>> GetAlbums()
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

        private async Task<List<Photo>> GetPhotos()
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