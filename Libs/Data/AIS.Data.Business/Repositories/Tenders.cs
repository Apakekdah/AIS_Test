using AIS.Data.Model;
using Hero.Business;
using Hero.Core.Interfaces;

namespace AIS.Data.Business.Repositories
{
    public class Tenders : BusinessClassBaseAsync<Tender>
    {
        public Tenders(IRepositoryAsync<Tender> repository, IUnitOfWorkAsync unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}