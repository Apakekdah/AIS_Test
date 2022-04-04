using AIS.Interfaces.Components;
using Hero;
using Hero.IoC;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

namespace AIS.LogFactory.NLogFactory
{
    class NLogProvider : ILogProvider
    {
        private NLog.LogFactory factory;

        public Hero.Core.Interfaces.ILogger GetLogger<T>() where T : class
        {
            return GetLogger(typeof(T).Name);
        }

        public Hero.Core.Interfaces.ILogger GetLogger(Type type)
        {
            type.ThrowIfNull(nameof(type));

            return GetLogger(type.Name);
        }

        public Hero.Core.Interfaces.ILogger GetLogger(string name)
        {
            Hero.Core.Interfaces.ILogger log;
            var life = GlobalIoC.Life;
            if (life == null)
            {
                if (factory == null)
                    throw new Exception("Log Must Be Intiate first");

                log = new Hero.Logger.NLogIt(name);
            }
            else
            {
                log = life.GetInstance<Hero.Core.Interfaces.ILogger>(new KeyValueParameter("name", name));
            }
            return log;
        }

        public void InitLog(string fileName)
        {
            factory = NLogBuilder.ConfigureNLog(fileName);
            factory.AutoShutdown = true;
            //var ftype = typeof(NLog.Targets.FileTarget).FullName;
            //foreach (NLog.Targets.FileTarget t in factory.Configuration.AllTargets.Where(c => c.GetType().FullName.Equals(ftype)))
            //{
            //    var fn = ((NLog.Layouts.SimpleLayout)t.FileName).FixedText;
            //    FileInfo fi = new FileInfo(fn);
            //    if (!fi.Directory.Exists)
            //    {
            //        fi.Directory.Create();
            //    }
            //    if (t.ArchiveFileName != null)
            //    {
            //        fn = ((NLog.Layouts.SimpleLayout)t.ArchiveFileName).FixedText;
            //        if (!fn.IsNullOrEmpty())
            //        {
            //            fi = new FileInfo(fn);
            //            if (!fi.Directory.Exists)
            //            {
            //                fi.Directory.Create();
            //            }
            //        }
            //    }
            //    //t.FileName.
            //}
        }

        public void ShutDown()
        {
            factory.Flush();
            factory.Shutdown();
        }

        public IHostBuilder ActivateLog(IHostBuilder builder)
        {
            return builder.UseNLog();
        }
    }
}
