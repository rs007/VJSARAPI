namespace VJSARAPI.Models
{
    public class Combine
    {
        //public IEnumerable<Album> Albums { get; set; }
        //public IEnumerable<Photo> Photos { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
