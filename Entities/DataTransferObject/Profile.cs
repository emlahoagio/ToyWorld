using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Biography { get; set; }
        public string Gender { get; set; }
    }
}
