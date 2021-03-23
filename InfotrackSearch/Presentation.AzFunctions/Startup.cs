using Application;
using Application.Behaviour;
using Application.BusinessLogic;
using Application.Interface;
using Infrastructure;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presentation.AzFunctions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

//[assembly: FunctionsStartup(typeof(Startup))]
namespace Presentation.AzFunctions
{    
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Startup()
        {

        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            var applicationAssembly = typeof(AutofacModule).GetTypeInfo().Assembly;
            //builder.Services
            builder.Services.AddScoped<IStaticSearch, StaticSearch>();
            builder.Services.AddScoped<IGoogleSearch, GoogleSearch>();
            builder.Services.AddScoped<IStaticSearchService, StaticSearchService>();
            builder.Services.AddScoped<IGoogleSearchService, GoogleSearchService>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            builder.Services.AddMediatR(applicationAssembly);
            builder.Services.AddControllers();
            //builder.Services.AddApplicationLayer();
        }
    }
}
