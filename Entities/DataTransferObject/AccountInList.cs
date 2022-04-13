using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class AccountInList
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public int Role { get; set; }
    }
}
