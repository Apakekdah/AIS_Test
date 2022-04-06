using Hero;
using Hero.Core.Commons;
using Hero.IoC;
using Microsoft.AspNetCore.Http;
using Ride.Handlers.Context;
using Ride.Handlers.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.Handlers.Filters
{
    public class AuthenticateUser : Disposable, IAuthenticationFilter
    {
        private readonly IHttpContextAccessor context;

        public AuthenticateUser(IDisposableIoC life)
        {
            context = life.GetInstance<IHttpContextAccessor>();
        }

        public bool CanHandle(CommandInvokerContext invokerContext)
        {
            if (context.IsNull())
            {
                throw new NullReferenceException("Context accessor not registered");
            }
            return true;
        }

        public Task OnAuthenticateAsync(AuthenticationFilterContext filterContext, CancellationToken cancellation)
        {
            var user = context.HttpContext.User;

            var claimUser = user.Claims.FirstOrDefault(f => f.Type.Equals(ClaimTypes.Name));
            if (claimUser.IsNull())
            {
                filterContext.Authenticated = false;
                return ReturnTask;
            }

            filterContext.Authenticated = true;
            filterContext.UserName = claimUser.Value;

            return ReturnTask;
        }

        private Task ReturnTask => Task.FromResult(0);
    }
}
