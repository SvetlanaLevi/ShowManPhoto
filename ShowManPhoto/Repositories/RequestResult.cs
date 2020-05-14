using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowManPhoto.Repositories
{
    public class RequestResult<T>
    {
        public T RequestData { get; set; }
        public bool IsOkay { get; set; }
        public string ExMessage { get; set; }
    }
}
