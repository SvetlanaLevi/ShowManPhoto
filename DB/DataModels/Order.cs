using System;
using System.Collections.Generic;

namespace DB
{
    public class Order
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        //public int AlbumId { get; set; }
        public DateTime? OrderDate { get; set; }
        public List<OrderedPhoto> OrderedPhotos { get; set; }
    }
}