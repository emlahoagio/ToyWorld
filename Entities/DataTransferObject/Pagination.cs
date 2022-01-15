using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class Pagination<T> where T:class
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
