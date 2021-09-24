using Esourcing.Sourcing.Data;
using Esourcing.Sourcing.Data.Interfaces;
using Esourcing.Sourcing.Repositories;
using Esourcing.Sourcing.Repositories.Interfaces;
using Esourcing.Sourcing.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.Sourcing
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
            services.AddControllers();
            services.Configure<SourcingDatabaseSettings>(Configuration.GetSection(nameof(SourcingDatabaseSettings)));

            #region Project Dependencies
            services.AddSingleton<ISourcingDatabaseSettings>(x=>x.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);
            services.AddTransient<ISourcingContext,SourcingContext>();
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IBidRepository, BidRepository>();
            #endregion

            #region Swagger Dependencies
            services.AddSwaggerGen(s=> 
            {
                s.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ESourcing.Sourcing", Version = "v1" });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c=> 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json","Sourcing API V1");
            });
        }
    }
}
