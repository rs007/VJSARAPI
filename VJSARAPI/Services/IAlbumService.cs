using VJSARAPI.Models;

namespace VJSARAPI.Services
{
    public interface IAlbumService
    {
        public Task<List<Album>> GetAlbums();
        public Task<List<Photo>> GetPhotos();
    }
}
