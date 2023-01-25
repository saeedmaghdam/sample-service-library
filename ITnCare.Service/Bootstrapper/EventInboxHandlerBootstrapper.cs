using ITnCare.Service.Event;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ITnCare.Service.Bootstrapper
{
    internal class EventInboxHandlerBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal EventInboxHandlerBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            _bootstrapperModel.Services.AddSingleton<IEventInboxHandler, EventInboxHandler>();
            return Task.CompletedTask;
        }
    }
}
