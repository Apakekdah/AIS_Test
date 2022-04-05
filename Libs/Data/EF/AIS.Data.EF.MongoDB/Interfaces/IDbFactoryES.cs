using Hero.Core.Interfaces;

namespace AIS.Data.EF.MongoDB.Interfaces
{
    interface IDbFactoryES : IDatabaseFactory<IMongoContext>
    {
    }
}