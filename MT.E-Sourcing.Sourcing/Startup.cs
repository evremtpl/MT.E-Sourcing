using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MT.E_Sourcing.Sourcing.Data.Concrete;
using MT.E_Sourcing.Sourcing.Data.Interfaces;
using MT.E_Sourcing.Sourcing.Data.Settings.Concrete;
using MT.E_Sourcing.Sourcing.Data.Settings.Interface;
using MT.E_Sourcing.Sourcing.Service.Concrete;
using MT.E_Sourcing.Sourcing.Service.Interfaces;

namespace MT.E_Sourcing.Sourcing.API
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
           

            services.Configure<SourcingDatabaseSettings>(Configuration.GetSection(nameof(SourcingDatabaseSettings)));
            services.AddSingleton<ISourcingDatabaseSettings>(sp => //Db conf larý bir kere setleneceði için singleton lifecycle kullanýldý.
            sp.GetRequiredService<IOptions<SourcingDatabaseSettings>>().Value);


            #region DI

            services.AddTransient<ISourcingContext, SourcingContext>(); // her istekte yeni bir object olurturmak için bu lifecycle.
            services.AddTransient<IAuctionRepository, AuctionRepository>();
            services.AddTransient<IBidRepository, BidRepository>();
            services.AddTransient<IAuctionService, AuctionService>();
            services.AddTransient<IBidService, BidService>();
            #endregion
            #region Swagger Dependencies
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ESourcing.Sourcing",
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "M.T."));
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
