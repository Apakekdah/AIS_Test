using MongoDB.Driver;

namespace AIS.Data.EF.MongoDB.Interfaces
{
    interface IMongoTable<T> where T : class
    {
        IClientSessionHandle Session { get; }
        IMongoCollection<T> Collection { get; }
    }
}