using System.Collections.Generic;

namespace ShowManPhoto.Models
{
    public class OrderedPhotosInputModel
    {
        public int UserId { get; set; }
        public List<SelectedItem> SelectedItems { get; set; }

    }

    public class SelectedItem
    {
        public int PhotoId { get; set; }
        public bool IsForPrint { get; set; }
        public int? PrintFormatId { get; set; }
        public string Comment { get; set; }
    }
}