using Cargos.API.DataContract;
using Cargos.API.Service;
using Cargos.API.Validator;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cargos
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
            services.AddDbContext<ApplicationDBContext>(option => option.UseLazyLoadingProxies().UseSqlServer(Configuration.GetSection("AppSettings").GetSection("ConnectionString").Value));

            // IoC repositories
            services.AddTransient<ICargoRepository, CargoRepository>();
            services.AddTransient<IFacturaRepository, FacturaRepository>();
            services.AddTransient<IEventoRepository, EventoRepository>();

            // IoC Validators
            services.AddSingleton<IValidator<EventoInputDataContract>, EventoInputDataContractValidator>();
            services.AddSingleton<IValidator<CargoUpdateDataContract>, CargoUpdateDataContractValidator>();

            // IoC Services
            services.AddTransient<ICargoService, CargoService>();
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
