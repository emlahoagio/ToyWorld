using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository.Services
{
    public class JwtServices : IJwtSupport
    {
        private readonly IConfiguration configuration;

        public JwtServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(int role, int accountId)
        {
            var Claims = new List<Claim>
            {
                new Claim("Role", role.ToString()),
                new Claim("AccountId", accountId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(configuration["Jwt:MinutesExprise"])).AddDays(int.Parse(configuration["Jwt:DateExprise"])),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
