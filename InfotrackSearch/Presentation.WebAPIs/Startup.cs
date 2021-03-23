 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.BusinessLogic;
using Application.Interface;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Presentation.WebAPIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => c.AddPolicy("CorsAllowAll1", ApplicationBuilder =>
            {
                ApplicationBuilder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddScoped<IStaticSearch, StaticSearch>();
            services.AddScoped<IGoogleSearch, GoogleSearch>();
            services.AddScoped<IStaticSearchService, StaticSearchService>();
            services.AddScoped<IGoogleSearchService, GoogleSearchService>();
            services.AddControllers();
            services.AddApplicationLayer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsAllowAll1");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
