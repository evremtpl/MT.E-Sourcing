using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MT.E_Sourcing.Common.RabbitMq.Connection.Concrete;
using MT.E_Sourcing.Common.RabbitMq.Connection.Interfaces;
using MT.E_Sourcing.Order.Application;
using MT.E_Sourcing.Order.Consumers;
using MT.E_Sourcing.Order.Extentions;
using MT.E_Sourcing.Order.Infrastructure;
using RabbitMQ.Client;

namespace MT.E_Sourcing.Order
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

            services.AddControllers();
            services.AddInfrastructure(Configuration);
            services.AddApplication();

            services.AddAutoMapper(typeof(Startup));

            #region EventBus
            services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<RabbitMqPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["EventBus:HostName"]


                };

                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:UserName"]))
                {
                    factory.UserName = Configuration["EventBus:UserName"];

                }
                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:Password"]))
                {
                    factory.Password = Configuration["EventBus:Password"];

                }
                var retryCount = 5;
                if (!string.IsNullOrWhiteSpace(Configuration["EventBus:RetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBus:RetryCount"]);

                }
                return new RabbitMqPersistentConnection(factory, retryCount, logger);
            });

            services.AddSingleton<EventBusOrderCreateConsumer>();
            #endregion




            #region Swagger Dependencies

            services.AddSwaggerGen(c=> {

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Order API", Version = "v1" });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            app.UseRabbitMqListener();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MT. Order API v1");
            
            });
        }
    }
}
