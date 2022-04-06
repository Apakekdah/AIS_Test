using AIS.Commands.API;
using AIS.Data.Business.Repositories;
using AIS.Data.Entity;
using Hero;
using Hero.IoC;
using Ride.Handlers.Handlers;
using Ride.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.Handlers.TenderCmd.D
{
    public partial class Handler : CommandHandlerBase<TenderCommandD, bool>
    {
        private readonly IDisposableIoC life;
        private readonly IMappingObject map;

        public Handler(Config config) :
            base(config)
        {
            life = config.Life;
            map = life.GetInstance<IMappingObject>();
        }

        public override Task<bool> Validate(TenderCommandD command)
        {
            var user = User.GetActiveUser();
            if (user.IsNullOrEmptyOrWhitespace())
            {
                throw new NullReferenceException("Can't found active user in session");
            }
            return Task.FromResult(true);
        }

        public override async Task<bool> Execute(TenderCommandD command, CancellationToken cancellation)
        {
            using (var scope = life.New)
            {
                var bllUser = scope.GetInstance<Users>();

                if (!await bllUser.IsUserOk(User.GetActiveUser()))
                {
                    throw new Exception("Active user is not valid");
                }

                var bll = scope.GetInstance<Tenders>();

                Tender tender = await bll.GetByTenderID(command.ID);

                if (tender.IsNull())
                {
                    throw new Exception($"Failed to read data for '{command.CommandProcessor}' with Id '{command.ID}'");
                }

                await bll.Delete(tender);

                var changes = await bll.Commit();

                if (changes > 0)
                    return true;

                return false;
            }
        }
    }
}