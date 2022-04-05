using Hero.IoC;
using Hero.IoC.Autofac;
using Hero.IoC.Autofac.LoggerModule;
using Hero.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AIS
{
    public static class AISServiceProviderFactory
    {
        public static IHostBuilder UseAISServiceProviderFactory(this IHostBuilder host)
        {
            return host.UseServiceProviderFactory(new IoCServiceProviderFactory());
        }

        public static void RegisteringHeroServices(this IBuilderIoC builder, IConfiguration configuration)
        {
            builder.RegisterModule<LogModule>();

            builder.RegisterLog<NLogIt>();

            //var registerAsms = new[] {
            //        // Api registration
            //        typeof(Business.BootstrapBusiness).Assembly,
            //        typeof(MongoDBEF.BootstrapMongoEF).Assembly,
            //        typeof(Mappers.BootstrapMapper).Assembly,
            //        typeof(API.Handlers.BootstrapApiHandlers).Assembly,
            //        typeof(Rule.BootstrapRules).Assembly,
            //        //// Broker
            //        typeof(SimpleMsgBroker.BootstrapMessageBroker).Assembly,
            //        //typeof(RabbitMQMsgBroker.BootstrapMessageBroker).Assembly
            //        //typeof(MT_ASB.BootstrapMessageBroker).Assembly
            //    };

            //builder.RegisterAllBootstrapLoaderBuilder(registerAsms);
        }
    }
}
