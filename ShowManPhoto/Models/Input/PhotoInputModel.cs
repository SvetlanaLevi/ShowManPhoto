using Microsoft.AspNetCore.Http;

namespace ShowManPhoto.Models
{
    public class PhotoInputModel
    {
        public IFormFile File { get; set; }
        public int AlbumId { get; set; }

    }
}