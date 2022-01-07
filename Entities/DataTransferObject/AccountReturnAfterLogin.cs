using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class AccountReturnAfterLogin
    {
        public int AccountId { get; set; }
        public string Avatar { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
    }
}
