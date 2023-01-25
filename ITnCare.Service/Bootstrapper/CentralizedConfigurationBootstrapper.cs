using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class CentralizedConfigurationBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal CentralizedConfigurationBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap<T>(bool optional, bool reloadOnChange) where T : class
        {
            var configuration = Helper.LoadAppSettings(_bootstrapperModel.ServiceName, _bootstrapperModel.ConsulUrl, optional, reloadOnChange);
            _bootstrapperModel.Services.Configure<T>(configuration.Result.GetSection(typeof(T).Name));

            return Task.CompletedTask;
        }
    }
}
