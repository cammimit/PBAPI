using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PBAPI.Models;
using PBAPI.Services;

namespace PBAPI
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
            // requires using Microsoft.Extensions.Options
            services.Configure<PersonStoreDatabaseSettings>(
                Configuration.GetSection(nameof(PersonStoreDatabaseSettings)));

            services.AddSingleton<IPersonStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PersonStoreDatabaseSettings>>().Value);

            services.AddSingleton<PersonService>();

            services.AddControllers();

            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title="PBA Api",
                    Description = "API Test"

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
         
            app.UseSwagger();

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json","My Test API V1");
           });

            app.UseRouting();

            app.UseAuthorization();

           app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
