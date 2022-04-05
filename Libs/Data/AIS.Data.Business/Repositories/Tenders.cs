using AIS.Data.Entity;
using Hero.Business;
using Hero.Core.Interfaces;
using System.Threading.Tasks;

namespace AIS.Data.Business.Repositories
{
    public class Tenders : BusinessClassBaseAsync<Tender>
    {
        public Tenders(IRepositoryAsync<Tender> repository, IUnitOfWorkAsync unitOfWork) : base(repository, unitOfWork)
        {
        }

        public Task<Tender> GetByTenderID(string id)
        {
            return Get(f => f.ID == id);
        }
    }
}