using AIS.Commands.API;
using AIS.Data.Business.Repositories;
using Hero;
using Hero.IoC;
using Ride.Handlers.Handlers;
using Ride.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.Handlers.AuthenticateCmd.Login
{
    public partial class Handler : CommandHandlerBase<AuthenticateCommand, Model.Models.AuthenticateResponse>
    {
        private readonly IDisposableIoC life;
        private readonly IMappingObject map;

        public Handler(Config config) :
            base(config)
        {
            life = config.Life;
            map = life.GetInstance<IMappingObject>();
        }

        public override async Task<Model.Models.AuthenticateResponse> Execute(AuthenticateCommand command, CancellationToken cancellation)
        {
            using (var scope = life.New)
            {
                var bll = scope.GetInstance<Users>();

                var userEntity = await bll.GetById(command.User);

                if (userEntity.IsNull())
                    throw new Exception($"User '{command.User}'");
                else if (!userEntity.IsActive)
                    throw new Exception($"User '{command.User}' not active");

                return new Model.Models.AuthenticateResponse
                {
                    User = command.User,
                    Create = DateTime.Now,
                    Session = "123"
                };
            }
        }
    }
}