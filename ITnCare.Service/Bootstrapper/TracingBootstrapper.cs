using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ITnCare.Service.Bootstrapper
{
    internal class TracingBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        public TracingBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            if (!_bootstrapperModel.GlobalOptions.TracingEnabled && !_bootstrapperModel.ServiceOptions.TracingEnabled)
                return Task.CompletedTask;

            _bootstrapperModel.Services.AddOpenTelemetryTracing(configure =>
            {
                configure.AddAspNetCoreInstrumentation();
                configure.AddSource($"{_bootstrapperModel.ServiceName}_ActivitySource");
                configure.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(_bootstrapperModel.ServiceName));
                configure.AddJaegerExporter(opts =>
                {
                    opts.AgentHost = _bootstrapperModel.GlobalOptions.Jaeger.AgentHost;
                    opts.AgentPort = _bootstrapperModel.GlobalOptions.Jaeger.AgentPort;
                    opts.ExportProcessorType = ExportProcessorType.Simple;
                });
            });
            return Task.CompletedTask;
        }
    }
}
