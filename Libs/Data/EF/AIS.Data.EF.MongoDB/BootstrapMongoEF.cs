using Hero.Core.Interfaces;
using Hero.IoC;
using MongoDB.Bson.Serialization.Conventions;
using Ride.Models.Entities;
using AIS.Data.EF.MongoDB.Contexts;
using AIS.Data.EF.MongoDB.Interfaces;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AIS.Data.EF.MongoDB
{
    public class BootstrapMongoEF : IBootstrapLoader<IBuilderIoC>
    {
        internal const string TABLE_MAPS = "tableMaps";

        private void MongoDBConvetion()
        {
            var convention = new ConventionPack
            {
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
            ConventionRegistry.Register("All", convention, f => true);
        }

        public Task Run(IBuilderIoC container)
        {
            container.Register<IMongoContext, MongoContext>(ScopeIoC.Lifetime);
            container.RegisterAsImplement<DbFactoryES>(ScopeIoC.Lifetime);
            container.RegisterAsImplement<UoW>(ScopeIoC.Lifetime);

            // Register repository
            container
                .RegisterAsImplement<RepositoryBase<ChannelMapping>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<ProductData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<SupplierData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<AddOnData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<VariantData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<ProductVariableData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<RequiredInfoFieldData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<FieldData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<AvailabilityData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<PickupData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<ProductMappingData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<RuleData>>(ScopeIoC.Lifetime)
                .RegisterAsImplement<RepositoryBase<BookingData>>(ScopeIoC.Lifetime)
                ;

            container.RegisterGeneric(typeof(MongoTable<>), typeof(IMongoTable<>));

            container.RegisterAsSelf<ConcurrentBag<string>>(TABLE_MAPS, ScopeIoC.Singleton);

            MongoDBConvetion();

            container.Register(fn =>
            {
                return new MongoTabelRunning();
            }, ScopeIoC.Singleton);

            return Task.FromResult(0);
        }

        public Task Stop()
        {
            return Task.FromResult(0);
        }
    }
}