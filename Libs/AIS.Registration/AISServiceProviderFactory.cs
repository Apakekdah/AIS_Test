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

        public static void RegisteringAISServicesDevelopment(this IBuilderIoC builder, IConfiguration configuration)
        {
            RegisteringAISServices(builder, configuration);
        }

        public static void RegisteringAISServicesProduction(this IBuilderIoC builder, IConfiguration configuration)
        {
            RegisteringAISServices(builder, configuration);
        }

        static void RegisteringAISServices(this IBuilderIoC builder, IConfiguration configuration)
        {
            builder.RegisterModule<LogModule>();

            builder.RegisterLog<NLogIt>();

            var registerAsms = new[] {
                    // Api registration
                    typeof(Data.Business.BootstrapBusiness).Assembly,
                    typeof(Data.EF.MongoDB.BootstrapMongoEF).Assembly,
                    typeof(Mappers.BootstrapMapper).Assembly,
                    typeof(API.Handlers.BootstrapApiHandlers).Assembly,
                };

            builder.RegisterAllBootstrapLoaderBuilder(registerAsms);
        }
    }
}
