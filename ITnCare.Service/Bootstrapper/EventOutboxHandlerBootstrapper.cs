using ITnCare.Service.Event;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class EventOutboxHandlerBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal EventOutboxHandlerBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            _bootstrapperModel.Services.AddSingleton<IEventOutboxHandler, EventOutboxHandler>();
            return Task.CompletedTask;
        }
    }
}
