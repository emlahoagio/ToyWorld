using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class ChangePwParameters
    {
        public string old_password { get; set; }
        public string new_password { get; set; }
    }
}
