using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using MT.E_Sourcing.Order.Consumers;
using Microsoft.Extensions.DependencyInjection;


namespace MT.E_Sourcing.Order.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static EventBusOrderCreateConsumer Listener { get; set; }

        public static IApplicationBuilder UseRabbitMqListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusOrderCreateConsumer>();

            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();
            life.ApplicationStarted.Register(OnStarted);

            life.ApplicationStopping.Register(OnStopping);

            return app;
        }

        public static void OnStarted()
        {
            Listener.Consume();
        }

        public static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
