using Hero.IoC;
using Microsoft.AspNetCore.Mvc;
using Ride.Interfaces;

namespace AIS.API.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController
    {
        private readonly IDisposableIoC life;
        private readonly IMappingObject map;

        public LoginController(IDisposableIoC life)
        {
            this.life = life;
            map = life.GetInstance<IMappingObject>();
        }

        //public async Task<IActionResult> Login(AuthenticateUser model, CancellationToken cancellation)
        //{
        //    var invoker = Life.GetInstance<ICommandInvoker<UserCommand, User>>();
        //    using (var cmd = Map.Get<UserCommand>(model, (src, dest) => dest.CommandProcessor = Commands.CommandProcessor.Add))
        //    {
        //        return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
        //    }
        //}

        //[HttpDelete("{userId}")]
        //public async Task<IActionResult> DeleteUser(string userId, CancellationToken cancellation)
        //{
        //    var invoker = Life.GetInstance<ICommandInvoker<UserCommand, User>>();
        //    using (var cmd = new UserCommand { UserID = userId, CommandProcessor = Commands.CommandProcessor.Delete })
        //    {
        //        return (await invoker.Invoke(cmd, cancellation)).ToContentJson();
        //    }
        //}
    }
}
