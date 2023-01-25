using ITnCare.Service.Event;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;

namespace ITnCare.Service.Bootstrapper
{
    public class EventInboxBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal EventInboxBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap(Action<IEventInboxBuilder> builder) 
        {
            if (!_bootstrapperModel.ServiceOptions.ConnectionStrings.ContainsKey("MongoDB"))
                throw new Exception("MongoDB connection string not found.");

            builder(new EventInboxBuilder(_bootstrapperModel.Services, _bootstrapperModel.ServiceName, _bootstrapperModel.ServiceOptions.ConnectionStrings["MongoDB"]));

            return Task.CompletedTask;
        }
    }
}
