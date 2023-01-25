using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class HealthCheckBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal HealthCheckBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            _bootstrapperModel.Services.AddHealthChecks();
            Context.IsHealthCheckEnabled = true;
            return Task.CompletedTask;
        }
    }
}
