﻿using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pagos.API.DataContract;
using Pagos.API.Service;
using Pagos.API.Validator;
using Pagos.Infrastructure;
using Pagos.Infrastructure.Repository;

namespace Pagos
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // IoC Context 
            services.AddDbContext<ApplicationDBContext>(option => option.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FACTURACION;"));

            // IoC repositories
            services.AddTransient<IPagoRepository, PagoRepository>();

            // IoC Validators
            services.AddSingleton<IValidator<PagoInputDataContract>, PagoInputDataContractValidator>();

            // IoC Services
            services.AddTransient<IPagoService, PagoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDBContext applicationDBContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            applicationDBContext.Database.EnsureCreated();
            app.UseMvc();
        }
    }
}