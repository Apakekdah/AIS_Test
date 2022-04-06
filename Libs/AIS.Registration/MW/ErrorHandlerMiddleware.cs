using Hero.Core.Interfaces;
using Hero.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AIS.Registration.MW
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        protected ILogger Log { get; private set; }

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            Log = GlobalIoC.Life.GetLogger(this.GetType());
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Log?.Error("Error while invoke next delegate", ex);

                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = Hero.HeroSerializer.Serializer.Serialize(new { Json = ex?.Message });

                await response.WriteAsync(result);
            }
        }
    }
}