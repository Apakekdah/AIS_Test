using Hero;
using AIS.Data.EF.MongoDB.Contexts;
using AIS.Data.EF.MongoDB.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace AIS.Data.EF.MongoDB
{
    class MongoTabelRunning
    {
        bool isRunning;

        public MongoTabelRunning()
        {
            Run();
        }

        public void Run()
        {
            if (isRunning) return;

            isRunning = true;

            Assembly[] asms = new[] { this.GetType().Assembly };
            Type intfs = typeof(IMongoTableConfigurable<>);

            asms.Each(asm =>
            {
                asm.GetTypes().Where(c => c.IsClass &&
                                intfs.IsClosesAsType(c) &&
                                !typeof(MongoTableConfigurator<>).Name.Equals(c.Name))
                    .Each(cls =>
                    {
                        var meth = cls.GetMethod("Apply");
                        if (meth != null)
                        {
                            var instance = Activator.CreateInstance(cls);
                            meth.Invoke(instance, null);
                        }
                    });
            });
        }
    }
}