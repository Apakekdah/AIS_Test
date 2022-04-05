using AIS.Data.EF.MongoDB.Contexts;
using AIS.Data.Entity;

namespace AIS.Data.EF.MongoDB.Configure
{
    class UserDataConfig : MongoTableConfigurator<User>
    {
    }
}