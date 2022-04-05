using AIS.Data.Entity;
using Hero.Business;
using Hero.Core.Interfaces;
using System.Threading.Tasks;

namespace AIS.Data.Business.Repositories
{
    public class Users : BusinessClassBaseAsync<User>
    {
        public Users(IRepositoryAsync<User> repository, IUnitOfWorkAsync unitOfWork) : base(repository, unitOfWork)
        {
        }

        public Task<User> GetUser(string id)
        {
            return Get(c => c.UserID == id && c.IsActive);
        }
    }
}