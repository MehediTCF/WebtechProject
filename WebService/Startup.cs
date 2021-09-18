using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Configurations;

namespace WebService
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
            // *** To Migration Go to Repositories.DataContext and follow instructions ***
            services.AddDbContext<DataContext>(options =>
                options.UseMySQL(
                    Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("Migrations")));

            this.AddSwaggerDoc(services);


            services.AddScoped<IRepository, Repository>();
            services.AddControllers();
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()));
        }

        private void AddSwaggerDoc(IServiceCollection services)
        {
            services.AddSwaggerGen(e =>
            {
                e.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagementApi", Version = "v1" });
/*
                e.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });*/
                /*e.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id="Bearer"}
                        },
                        new string[] {}
                    }
                });*/
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection("SwaggerOptions").Bind(swaggerOptions);
            app.UseSwagger(op => op.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(op => { op.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description); });

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
