using Hero.Core.Commons;
using Hero.IoC;
using AIS.Data.EF.MongoDB.Interfaces;

namespace AIS.Data.EF.MongoDB
{
    class DbFactoryES : Disposable, IDbFactoryES
    {
        private readonly IDisposableIoC life;
        private IMongoContext dataContext;

        public DbFactoryES(IDisposableIoC life)
        {
            this.life = life;
        }

        public IMongoContext Get()
        {
            return (dataContext ??= life.GetInstance<IMongoContext>());
        }

        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}