using Hero.Core.Interfaces;
using Hero.IoC;
using Ride.Handlers;
using Ride.Handlers.Filters;
using System.Threading.Tasks;

namespace AIS.API.Handlers
{
    public class BootstrapApiHandlers : IBootstrapLoader<IBuilderIoC>
    {
        public Task Run(IBuilderIoC container)
        {
            // Register Authentication Filter 
            container.RegisterAssemblyTypes(RegistrationTypeIoC.AsImplement, null, ScopeIoC.Singleton, true, new[]
            {
                typeof(IAuthenticationFilter),

            }, typeof(BootstrapApiHandlers).Assembly);

            // Register Pre Invoke Filter
            container.RegisterAssemblyTypes(RegistrationTypeIoC.AsLook, null, ScopeIoC.Singleton, true, new[]
            {
                typeof(IPreInvocationFilter<>),

            }, typeof(BootstrapApiHandlers).Assembly);

            /// ############## Tender ##############
            container.Register(c => TenderCmd.CUD.Handler.CreateBuilder()
                .WithLife(c).Build().CreateInvoker(c), ScopeIoC.Singleton);
            container.Register(c => TenderCmd.Read.Handler.CreateBuilder()
                .WithLife(c).Build().CreateInvoker(c), ScopeIoC.Singleton);

            return Task.FromResult(0);
        }

        public Task Stop()
        {
            return Task.FromResult(0);
        }
    }
}