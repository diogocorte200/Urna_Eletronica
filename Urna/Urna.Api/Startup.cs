using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Urna.Domain.Mapping;
using Urna.Domain.Service;
using Urna.Domain.Service.Generic;
using Urna.Entity.Context;
using Urna.Entity.Repositories;
using Urna.Entity.Repositories.Interfaces;
using Urna.Entity.UnitofWork;

namespace Urna.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                      new OpenApiInfo
                      {
                          Title = "Webservices urna",
                          Version = "v1",
                          Description = "Webservices urna",
                          Contact = new OpenApiContact
                          {
                              Name = "Diogo Rodrigues",
                          }
                      });

            });


            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(Configuration["ConnectionStrings:clienteDB"]);
                config.UseSerializerSettings(new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            });


            if (Configuration["ConnectionStrings:UseInMemoryDatabase"] == "True")
                services.AddDbContext<UrnaContext>(opt => opt.UseInMemoryDatabase("TestDB-" + Guid.NewGuid().ToString()));
            else
                services.AddDbContext<UrnaContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:clienteDB"]));


            services.AddTransient<IUnitOfWork, UnitOfWork>();



            services.AddTransient(typeof(CandidatoService<,>), typeof(CandidatoService<,>));
            services.AddTransient(typeof(VotoService<,>), typeof(VotoService<,>));


            services.AddTransient(typeof(IServiceAsync<,>), typeof(GenericServiceAsync<,>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<ICandidatoRepository, CandidatoRepository>();
            services.AddTransient<IVotoRepository, VotoRepository>();

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddHttpContextAccessor();


            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Webservices clientes");
            });
        }
    }
}
