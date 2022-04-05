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

namespace AIS.API.Handlers.TenderCmd.CUD
{
    public partial class Handler : CommandHandlerBase<TenderCommandCUD, bool>
    {
        private readonly IDisposableIoC life;
        private readonly IMappingObject map;

        public Handler(Config config) :
            base(config)
        {
            life = config.Life;
            map = life.GetInstance<IMappingObject>();
        }

        public override Task<bool> Validate(TenderCommandCUD command)
        {
            if (command.Tender.IsNull())
            {
                throw new NullReferenceException("Tender object is not created");
            }
            return Task.FromResult(true);
        }

        public override async Task<bool> Execute(TenderCommandCUD command, CancellationToken cancellation)
        {
            using (var scope = life.New)
            {
                var bll = scope.GetInstance<Tenders>();

                Tender tender;
                bool isMarkAdd = false;

                switch (command.CommandProcessor)
                {
                    case Commands.CommandProcessor.Add:
                        tender = new Tender()
                        {
                            ID = Guid.NewGuid().ToString()
                        };
                        isMarkAdd = true;
                        break;
                    case Commands.CommandProcessor.Delete:
                    case Commands.CommandProcessor.Edit:
                        tender = await bll.GetByTenderID(command.ID);
                        break;
                    default:
                        throw new Exception($"Unsupported Processing '{command.CommandProcessor}' in command {Name}");
                }

                if (tender.IsNull())
                {
                    throw new Exception($"Failed to read data for '{command.CommandProcessor}' with Id '{command.Tender.ID}'");
                }

                tender.Name = command.Tender.Name;
                tender.ReferenceNumber = command.Tender.ReferenceNumber;
                tender.ReleaseDate = command.Tender.ReleaseDate;
                tender.ClosingDate = command.Tender.ClosingDate;
                tender.Details = command.Tender.Details;
                tender.CreatorID = command.Tender.CreatorID;

                if (isMarkAdd)
                    await bll.Add(tender);
                else
                    await bll.Update(tender);

                var changes = await bll.Commit();

                if (changes > 0)
                    return true;

                return false;
            }
        }
    }
}