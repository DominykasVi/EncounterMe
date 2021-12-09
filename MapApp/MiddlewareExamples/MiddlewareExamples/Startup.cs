using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using EncounterMe.Functions;
using EncounterMe.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MiddlewareExamples.Domain.Aspects;
using MiddlewareExamples.Domain.ServiceAgent;
using MiddlewareExamples.Domain.Services.WeatherService;
using MiddlewareExamples.WebApi.Middleware;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewareExamples
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewareExamples", Version = "v1" });
            });

            //services.AddScoped<IWeatherService, WeatherService>();
            //services.AddScoped<IWeatherServiceAgent, WeatherServiceAgent>();
            //services.AddSingleton(x => Log.Logger);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            builder.RegisterType<GameLogic>().As<IGame>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<WeatherService>().As<IWeatherService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<WeatherServiceAgent>().As<IWeatherServiceAgent>().InstancePerDependency();
            builder.Register(x => Log.Logger).SingleInstance();
            builder.RegisterType<LogAspect>().SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewareExamples v1"));
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<StatisticsMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
