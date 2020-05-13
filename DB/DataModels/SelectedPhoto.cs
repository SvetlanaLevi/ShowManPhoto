namespace DB
{
    public class SelectedPhoto
    {
        public int? Id { get; set; }
        public int OrderId { get; set; }
        public Photo Photo { get; set; }
        public bool IsForPrint { get; set; }
        public int? PrintFormatId { get; set; }
        public string Properties { get; set; }
        public string Comment { get; set; }
    }
}