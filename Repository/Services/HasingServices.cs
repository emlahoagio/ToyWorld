using Contracts.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Repository.Services
{
    public class HasingServices : IHasingServices
    {
        public string encriptSHA256(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
