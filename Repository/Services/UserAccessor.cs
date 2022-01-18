using Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int getAccountId()
        {
            return int.Parse(httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type == "AccountId").Value);
        }
    }
}
