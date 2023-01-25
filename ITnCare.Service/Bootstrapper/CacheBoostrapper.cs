using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class CacheBoostrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal CacheBoostrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            Bootstrap(_bootstrapperModel.ServiceName);
            return Task.CompletedTask;
        }

        internal Task Bootstrap(string instanceName)
        {
            if (string.IsNullOrEmpty(instanceName))
                throw new ArgumentNullException("InstanceName is required.");

            if (string.IsNullOrEmpty(instanceName.Trim()))
                throw new ArgumentNullException("InstanceName is required.");

            _bootstrapperModel.Services.AddDistributedMemoryCache();
            _bootstrapperModel.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = _bootstrapperModel.GlobalOptions.ConnectionStrings["RedisConnectionString"];
                options.InstanceName = instanceName;
            });

            return Task.CompletedTask;
        }
    }
}
