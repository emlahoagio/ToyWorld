using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Services
{
    public interface IHasingServices
    {
        public string encriptSHA256(string password);
    }
}
