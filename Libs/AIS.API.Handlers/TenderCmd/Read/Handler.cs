using AIS.Commands.API;
using AIS.Data.Business.Repositories;
using AIS.Data.Entity;
using Hero;
using Hero.IoC;
using Ride.Handlers.Handlers;
using Ride.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AIS.API.Handlers.TenderCmd.Read
{
    public partial class Handler : CommandHandlerBase<TenderCommandRA, IEnumerable<Model.Models.Tender>>
    {
        private readonly IDisposableIoC life;
        private readonly IMappingObject map;

        public Handler(Config config) :
            base(config)
        {
            life = config.Life;
            map = life.GetInstance<IMappingObject>();
        }

        public override Task<bool> Validate(TenderCommandRA command)
        {
            var user = User.GetActiveUser();
            if (user.IsNullOrEmptyOrWhitespace())
            {
                throw new NullReferenceException("Can't found active user in session");
            }
            return Task.FromResult(true);
        }

        public override async Task<IEnumerable<Model.Models.Tender>> Execute(TenderCommandRA command, CancellationToken cancellation)
        {
            using (var scope = life.New)
            {
                var bll = scope.GetInstance<Tenders>();

                Tender tender;
                IEnumerable<Tender> tenders = null;

                switch (command.CommandProcessor)
                {
                    case Commands.CommandProcessor.GetAll:
                        tenders = await bll.GetAll();
                        break;
                    case Commands.CommandProcessor.GetOne:
                        tender = await bll.GetByTenderID(command.ID);
                        if (!tender.IsNull())
                        {
                            tenders = new[] { tender };
                        }
                        break;
                    default:
                        throw new Exception($"Unsupported Processing '{command.CommandProcessor}' in command {Name}");
                }

                if (tenders.IsNull())
                    return null;

                return map.Get<List<Model.Models.Tender>>(tenders);
            }
        }
    }
}