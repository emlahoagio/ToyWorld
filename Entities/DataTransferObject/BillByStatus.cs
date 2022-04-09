using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class BillByStatus
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string SenderToy { get; set; }
        public string ReceiverToy { get; set; }
        public string PostTitle { get; set; }
        public int IdPost { get; set; }
        public int Status { get; set; }
        public DateTime DateCreate { get; set; }
        public List<ImageReturn> Images { get; set; }
    }
}
