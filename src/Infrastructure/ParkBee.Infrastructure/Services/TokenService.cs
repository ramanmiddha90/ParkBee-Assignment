using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkBee.Core.Application.Common.Interfaces;
using ParkBee.Core.Application.Common.Models;
using ParkBee.Core.Application.Token;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkBee.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResult GetToken(TokenRequestQuery query)
        {
            var user = Authenticate(query.UserName, query.Password);

            if (user == null)
                return null;

            var token = GetJwtSecurityToken(user);

            return new TokenResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }

        private JwtSecurityToken GetJwtSecurityToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return token;
        }
        private UserModel Authenticate(string username, string password) =>
         // For simple authentication we just compare username and password
         username.Equals(password)
             ? new UserModel
             {
                 Name = "Joe Soap",
                 Email = "joe@mailinator.com"
             }
             : default(UserModel);
    }
}
