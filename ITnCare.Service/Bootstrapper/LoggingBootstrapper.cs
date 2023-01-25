using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace ITnCare.Service.Bootstrapper
{
    internal class LoggingBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        public LoggingBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_bootstrapperModel.ServiceConfiguration)
                .CreateLogger();
            Log.Logger.BindProperty("Application", _bootstrapperModel.ServiceName, true, out var property);

            _bootstrapperModel.Services.AddLogging(config => config.AddSerilog());

            return Task.CompletedTask;
        }
    }
}
