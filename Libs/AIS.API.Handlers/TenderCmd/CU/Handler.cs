using AIS.Commands.API;
using AIS.Data.Business.Repositories;
using AIS.Data.Entity;
using Hero;
using Hero.IoC;
using Ride.Handlers.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.Handlers.TenderCmd.CU
{
    public partial class Handler : CommandHandlerBase<TenderCommandCU, bool>
    {
        private readonly IDisposableIoC life;
        private string activeUser;

        public Handler(Config config) :
            base(config)
        {
            life = config.Life;
        }

        public override Task<bool> Validate(TenderCommandCU command)
        {
            activeUser = User.GetActiveUser();
            if (activeUser.IsNullOrEmptyOrWhitespace())
            {
                throw new NullReferenceException("Can't found active user in session");
            }
            if (command.CommandProcessor == Commands.CommandProcessor.Edit)
            {
                if (command.ID.IsNullOrEmptyOrWhitespace())
                {
                    throw new NullReferenceException("Tender ID is required");
                }
            }
            return Task.FromResult(true);
        }

        public override async Task<bool> Execute(TenderCommandCU command, CancellationToken cancellation)
        {
            // Simple to show log
            Log.Debug($"User executed : {activeUser}");

            using (var scope = life.New)
            {
                var bllUser = scope.GetInstance<Users>();

                if (!await bllUser.IsUserOk(activeUser))
                {
                    throw new Exception("Active user is not valid");
                }

                var bll = scope.GetInstance<Tenders>();

                Tender tender;

                switch (command.CommandProcessor)
                {
                    case Commands.CommandProcessor.Add:
                        tender = new Tender()
                        {
                            ID = Guid.NewGuid().ToString()
                        };
                        break;
                    case Commands.CommandProcessor.Edit:
                        tender = await bll.GetByTenderID(command.ID);
                        break;
                    default:
                        throw new Exception($"Unsupported Processing '{command.CommandProcessor}' in command {Name}");
                }

                if (tender.IsNull())
                {
                    throw new Exception($"Failed to read data for '{command.CommandProcessor}' with Id '{command.ID}'");
                }

                tender.Name = command.Name;
                tender.ReferenceNumber = command.ReferenceNumber;
                tender.ReleaseDate = command.ReleaseDate;
                tender.ClosingDate = command.ClosingDate;
                tender.Details = command.Details;
                tender.CreatorID = activeUser;

                switch (command.CommandProcessor)
                {
                    case Commands.CommandProcessor.Add:
                        await bll.Add(tender);
                        break;
                    default:
                        await bll.Update(tender);
                        break;
                }

                var changes = await bll.Commit();

                if (changes > 0)
                    return true;

                return false;
            }
        }
    }
}