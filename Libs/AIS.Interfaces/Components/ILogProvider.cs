using Hero.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using System;

namespace AIS.Interfaces.Components
{
    public interface ILogProvider
    {
        void InitLog(string fileName);
        void ShutDown();
        ILogger GetLogger<T>() where T : class;
        ILogger GetLogger(Type type);
        ILogger GetLogger(string name);
        IHostBuilder ActivateLog(IHostBuilder builder);
    }
}