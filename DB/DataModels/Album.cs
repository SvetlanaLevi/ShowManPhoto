using System;
using System.Collections.Generic;

namespace DB
{
    public class Album
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Comment { get; set; }
        public List<Photo> Photos { get; set; }
    }
}