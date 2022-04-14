using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class UpdateAccountParameters
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Biography { get; set; }
        public string Gender { get; set; }
    }
}
