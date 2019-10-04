using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pagos.API;
using Pagos.API.Config;
using Pagos.API.DataContract;
using Pagos.API.Service;
using Pagos.API.Validator;
using Pagos.Infrastructure;
using Pagos.Infrastructure.Repository;
using Swashbuckle.AspNetCore.Swagger;

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

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() { Name = SwaggerConfiguration.ContactName, Url = SwaggerConfiguration.ContactUrl };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                   new Info
                                   {
                                       Title = SwaggerConfiguration.DocInfoTitle,
                                       Version = SwaggerConfiguration.DocInfoVersion,
                                       Description = SwaggerConfiguration.DocInfoDescription,
                                       Contact = contact
                                   }
                                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });

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
