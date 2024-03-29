using AIS.Registration.MW;
using Hero.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace AIS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment appEnv)
        {
            Configuration = configuration;
            Environment = appEnv;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                  .AddJsonOptions(opt =>
                  {
                      opt.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                      opt.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                      opt.JsonSerializerOptions.IgnoreNullValues = true;
                      opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                      opt.JsonSerializerOptions.Converters.Add(Hero.Core.Serializer.TextJson.Converter.DateTimeJsonConverter.Create());
                      opt.JsonSerializerOptions.Converters.Add(Hero.Core.Serializer.TextJson.Converter.DateTimeNullJsonConverter.Create());
                      opt.JsonSerializerOptions.Converters.Add(Hero.Core.Serializer.TextJson.Converter.TimeSpanJsonConverter.Create());
                      opt.JsonSerializerOptions.Converters.Add(Hero.Core.Serializer.TextJson.Converter.TimeSpanNullJsonConverter.Create());
                      opt.JsonSerializerOptions.Converters.Add(Hero.Core.Serializer.TextJson.Converter.ExceptionConverter.Create());
                      opt.JsonSerializerOptions.Converters.Add(Hero.Core.Serializer.TextJson.Converter.DictionaryObjectConverter.Create());
                      opt.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                  });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AIS API", Version = "v1" });
                c.ResolveConflictingActions(x => x.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "ais-sdk";
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "AIS API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "api_default",
                    pattern: "api/{controller=WeatherForecast}/{action=Get}/{id?}");
            });

            //var defaults = new DefaultFilesOptions().DefaultFileNames.Select(p => "/" + p);
            //app.Use(async (context, next) =>
            //{
            //    var path = context.Request.Path;
            //    if ((path == "/") || defaults.Any(p => p.Equals(path, System.StringComparison.InvariantCultureIgnoreCase)))
            //    {
            //        context.Response.Redirect("/Home");
            //        return;
            //    }
            //    await next();
            //});
        }

        public void ConfigureContainer(IBuilderIoC builder)
        {
            if (Environment.IsDevelopment())
            {
                builder.RegisteringAISServicesDevelopment(Configuration);
            }
            else
            {
                builder.RegisteringAISServicesProduction(Configuration);
            }
        }
    }
}
