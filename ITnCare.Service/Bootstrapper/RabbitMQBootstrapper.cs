using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using ITnCare.Service.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace ITnCare.Service.Bootstrapper
{
    internal class RabbitMQBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        public RabbitMQBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            InitializeGlobalChannels();
            InitializeServiceChannels();
            InitializeServices();

            return Task.CompletedTask;
        }

        private void InitializeGlobalChannels()
        {
            foreach (var rabbitMQOption in _bootstrapperModel.GlobalOptions.RabbitMQs)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMQOption.Value.Host,
                    Port = rabbitMQOption.Value.Port,
                    UserName = rabbitMQOption.Value.Username,
                    Password = rabbitMQOption.Value.Password
                };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                Context.RabbitMQChannels.Add($"{RabbitMQOptionsOrigin.Global}_{rabbitMQOption.Key}", channel);
            }
        }

        private void InitializeServiceChannels()
        {
            foreach (var rabbitMQOption in _bootstrapperModel.ServiceOptions.RabbitMQs)
            {
                var factory = new ConnectionFactory()
                {
                    HostName = rabbitMQOption.Value.Host,
                    Port = rabbitMQOption.Value.Port,
                    UserName = rabbitMQOption.Value.Username,
                    Password = rabbitMQOption.Value.Password
                };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                Context.RabbitMQChannels.Add($"{RabbitMQOptionsOrigin.Service}_{rabbitMQOption.Key}", channel);
            }
        }

        private void InitializeServices()
        {
            _bootstrapperModel.Services.AddSingleton<IRabbitMQSubscriber, RabbitMQSubscriber>();
            _bootstrapperModel.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();
        }
    }
}
