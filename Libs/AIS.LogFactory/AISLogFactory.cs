using AIS.LogFactory.NLogFactory;
using Hero.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using Ride.Interfaces;
using System;

namespace AIS
{
    public static class AISLogFactory
    {
        private static readonly ILogProvider logProvider = new NLogProvider();

        public static void InitLog(string fileName)
        {
            logProvider.InitLog(fileName);
        }

        public static void ShutDown()
        {
            logProvider.ShutDown();
        }

        public static ILogger GetLogger<T>()
            where T : class
        {
            return logProvider.GetLogger<T>();
        }

        public static ILogger GetLogger(Type type)
        {
            return logProvider.GetLogger(type);
        }

        public static ILogger GetLogger(string name)
        {
            return logProvider.GetLogger(name);
        }

        public static IHostBuilder UseAISLog(this IHostBuilder builder)
        {
            return logProvider.ActivateLog(builder);
        }
    }
}