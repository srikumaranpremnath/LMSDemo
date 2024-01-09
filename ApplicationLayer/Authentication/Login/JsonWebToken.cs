using Application.Authentication.Login;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Login
{
    public class JsonWebToken : IJsonWebToken
    {
        private readonly IConfiguration _configuration;
        public JsonWebToken(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public  string GenerateJWT(string username, string userType)
        {
            string key = _configuration.GetValue<string>("JWT:Secret");
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
                var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, userType)
                }
                ),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}
