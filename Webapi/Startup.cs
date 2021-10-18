using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Business;
using Business.Contracts;
using DataAccess;
using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Webapi
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
            AddRepositories(services);

            AddBusiness(services);

            services.AddControllers();

            AddSwagger(services);

            AddCors(services);
        }

        private static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                //ToDo: Add production CORS policy
                options.AddPolicy("development", builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }

        private static void AddBusiness(IServiceCollection services)
        {
            services.AddScoped(typeof(IBudgetBusiness), typeof(BudgetBusiness));
            services.AddScoped(typeof(IBookBusiness), typeof(BookBusiness));
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddDbContext<BookLibraryDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookLibrary"));

            services.AddScoped(typeof(IBudgetRepository), typeof(BudgetRepository));
            services.AddScoped(typeof(IBookRepository), typeof(BookRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("development");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookLibrary API v1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "BookLibrary API",
                    Version = "v1",
                    Description = "API Services for BookLibrary."
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(r => r.FullName);
            });
        }
    }
}
