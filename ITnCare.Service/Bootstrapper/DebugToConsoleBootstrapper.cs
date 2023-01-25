using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class DebugToConsoleBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal DebugToConsoleBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
#if DEBUG
            _bootstrapperModel.Services.AddHostedService<DebugWorker>();
#endif

            return Task.CompletedTask;
        }
    }
}
