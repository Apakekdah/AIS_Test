using MongoDB.Driver;
using AIS.Data.EF.MongoDB.Interfaces;

namespace AIS.Data.EF.MongoDB
{
    class MongoTable<T> : IMongoTable<T>
        where T : class
    {
        public MongoTable(IClientSessionHandle session, IMongoCollection<T> collection)
        {
            Session = session;
            Collection = collection;
        }

        public IClientSessionHandle Session { get; }
        public IMongoCollection<T> Collection { get; }
    }
}