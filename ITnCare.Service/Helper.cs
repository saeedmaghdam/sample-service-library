using Microsoft.Extensions.Configuration;
using Winton.Extensions.Configuration.Consul;

namespace ITnCare.Service
{
    internal static class Helper
    {
        internal static Task<IConfigurationRoot> LoadAppSettings(string? serviceName, string consulUrl, bool optional, bool reloadOnChange)
        {
            var appsettingsPath = "appsettings.json";
            if (!string.IsNullOrEmpty(serviceName))
                appsettingsPath = $"{serviceName}/{appsettingsPath}";

            var configurationBuilder = new ConfigurationBuilder().AddConsul(
                    appsettingsPath,
                    options =>
                    {
                        options.ConsulConfigurationOptions =
                            cco => { cco.Address = new Uri(consulUrl); };
                        options.Optional = optional;
                        options.ReloadOnChange = reloadOnChange;
                        options.OnLoadException = exceptionContext => { exceptionContext.Ignore = true; };
                    }
                );
            var configuration = configurationBuilder.Build();

            return Task.FromResult(configuration);
        }
    }
}
