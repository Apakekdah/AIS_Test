using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.ExceptionServices;
using System.Text;

namespace AIS
{
    public class Program
    {
        private static Hero.Core.Interfaces.ILogger Log;

        public static void Main(string[] args)
        {
            Hero.HeroSerializer.SetSerializer(Hero.Core.Serializer.TextJson.TJJson.Create(true));
            Hero.HeroSerializer.SetBinarySerializer(Hero.Core.Serializer.TextJson.TJBinary.Create());

            AISLogFactory.InitLog("nlog.config");

            CreateHostBuilder(args).Build().Run();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs ex)
        {
            string jsonData = null;

            if (ex.ExceptionObject != null)
            {
                jsonData = Hero.HeroSerializer.Serializer.Serialize(ex.ExceptionObject);
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("CurrentDomain_UnhandledException");
            sb.AppendLine(string.Concat("Object : ", jsonData));
            sb.AppendLine(string.Concat("Terminate : ", ex.IsTerminating));

            Log.Fatal(sb.ToString());

            sb.Clear();
        }

        static void CurrentDomain_FirstChanceException(object sender, FirstChanceExceptionEventArgs ex)
        {
            if (ex.Exception.Message.Contains(@"\Log\", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("----------------------------");
            sb.AppendLine(ex.Exception.Message);
            sb.AppendLine("----------------------------");
            sb.AppendLine(ex.Exception.StackTrace);

            Log.Error(sb.ToString());

            sb.Clear();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
