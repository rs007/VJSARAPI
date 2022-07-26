using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VJSARAPI.Models;
using VJSARAPI.Services;

namespace VJSARAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAlbumController : ControllerBase
    {
        //private readonly ILogger<PhotoAlbumController> _logger;
        //public PhotoAlbumController(ILogger<PhotoAlbumController> logger)
        //{
        //    _logger = logger;
        //}
        //public PhotoAlbumController() { }
        private readonly IAlbumService _albumService;
        public PhotoAlbumController()
        {
            _albumService = new AlbumService();
        }
        [HttpGet("GetCombine")]
        public async Task<IEnumerable<Combine>> GetCombine(int id)
        {           
            List<Album> albumslst = await _albumService.GetAlbums();
            List<Photo> photolst = await _albumService.GetPhotos();

            var query = from album in albumslst
                        join photo in photolst
                        on album.Id equals photo.AlbumId
                        where album.UserId == id
                        select new Combine
                        {
                            UserId = album.UserId,
                            Id = photo.Id,
                            AlbumId = photo.AlbumId,
                            Url = photo.Url,
                            ThumbnailUrl = photo.ThumbnailUrl,
                            Title = photo.Title
                        };
            return query;
        }

        [HttpGet("GetCombineResult")]
        public async Task<CombineResult> GetCombineResult(int id)
        {
            CombineResult combineResult = new CombineResult();
            List<Album> albumslst = await _albumService.GetAlbums();
            List<Photo> photolst = await _albumService.GetPhotos();

            var albumn = albumslst.Where(albumn => albumn.UserId == id).ToList();
            var query = from album in albumn 
                        join photo in photolst
                        on album.Id equals photo.AlbumId
                        where album.UserId == id
                        select photo;
            combineResult.Albums = albumn;
            combineResult.Photos = query;
            return combineResult;
        }
       
    }
}
