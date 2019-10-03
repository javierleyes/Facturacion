using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pagos.API.DataContract;
using Pagos.API.Infrastructure;
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

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // IoC Context 
            services.AddDbContext<ApplicationDBContext>(option => option.UseLazyLoadingProxies().UseSqlServer(Configuration.GetSection("AppSettings").GetSection("ConnectionString").Value));

            // IoC repository
            services.AddTransient<IPagoRepository, PagoRepository>();

            // IoC Validator
            services.AddSingleton<IValidator<PagoInputDataContract>, PagoInputDataContractValidator>();

            // IoC Service
            services.AddTransient<IPagoService, PagoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }
    }
}
