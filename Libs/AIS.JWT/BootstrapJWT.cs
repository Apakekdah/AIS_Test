using AIS.JWT;
using AIS.JWT.Models;
using Hero.Core.Interfaces;
using Hero.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AIS.Mappers
{
    public class BootstrapMapper : IBootstrapLoader<IBuilderIoC>
    {
        public Task Run(IBuilderIoC container)
        {
            container.RegisterAsImplement<UserAuthorization>();

            var pathFile = "appsettings.json";
            var builder = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile(pathFile, optional: true, reloadOnChange: true);

            var cfg = builder.Build();

            var jwtCfg = new JwtConfig();

            cfg.GetSection(JwtConfig.SectionName).Bind(jwtCfg);

            container.RegisterInstance(jwtCfg);

            IServiceCollection service = new ServiceCollection();
            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                var key = Encoding.UTF8.GetBytes(jwtCfg.Key);
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });

            var builderD = (IBuilderIoCDI)builder;

            builderD.Populate(service);

            return Task.FromResult(0);
        }

        public Task Stop()
        {
            return Task.FromResult(0);
        }


    }
}