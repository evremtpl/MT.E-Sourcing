using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using MT.E_Sourcing.Data.Interfaces;
using MT.E_Sourcing.Data.Concrete;
using MT.E_Sourcing.Data.Settings.Concrete;
using MT.E_Sourcing.Data.Settings.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MT.E_Sourcing.Products
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

           


            #region Configuration Dependencies
            services.Configure<ProductDatabaseSettings>(Configuration.GetSection(nameof(ProductDatabaseSettings)));
            services.AddSingleton<IProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
            #endregion

            #region Project Dependencies
            services.AddTransient<IProductContext, ProductContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion

            #region Swagger Dependencies
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ESourcing.Products",
                    Version = "v1"
                });
            });
            #endregion




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               app.UseSwagger();
               app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MerveTOPAL.E_Sourcing.Products v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
