namespace DB
{
    public class Photo
    {
        public int? Id { get; set; }
        public int? AlbumId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
}