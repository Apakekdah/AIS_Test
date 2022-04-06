using AIS.Interface;
using AIS.JWT.Models;
using AIS.Model.Models;
using Hero.IoC;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AIS.JWT
{
    public class UserAuthorization : IUserLogin, IUserLogoff
    {
        private readonly IDisposableIoC life;
        private readonly JwtConfig jwtCfg;

        public UserAuthorization(IDisposableIoC life)
        {
            this.life = life;
            jwtCfg = life.GetInstance<JwtConfig>();
        }

        public AuthenticateResponse Login(AuthenticateUser authenticate, IDictionary<string, object> properties)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(jwtCfg.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(jwtCfg.ExpiredMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticateResponse
            {
                User = authenticate.User,
                Create = DateTime.UtcNow,
                Session = tokenHandler.WriteToken(token)
            };
        }

        public void LogOff(string token)
        {
            throw new NotImplementedException();
        }
    }
}