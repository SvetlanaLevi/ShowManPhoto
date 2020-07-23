namespace DB
{
    public class OrderedPhoto
    {
        public int? Id { get; set; }
        public int OrderId { get; set; }
        public int PhotoId { get; set; }
        public bool IsForPrint { get; set; }
        public int? PrintFormatId { get; set; }
        public string Comment { get; set; }
    }
}