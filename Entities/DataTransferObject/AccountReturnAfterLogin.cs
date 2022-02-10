using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class AccountReturnAfterLogin
    {
        public int AccountId { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
    }
}
