using ITnCare.Service.Event;
using ITnCare.Service.Framework;
using ITnCare.Service.Models;

namespace ITnCare.Service.Bootstrapper
{
    public class EventOutboxBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal EventOutboxBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap(Action<IEventOutboxBuilder> builder) 
        {
            if (!_bootstrapperModel.ServiceOptions.ConnectionStrings.ContainsKey($"MongoDB"))
                throw new Exception("MongoDB connection string not found.");

            builder(new EventOutboxBuilder(_bootstrapperModel.Services, _bootstrapperModel.ServiceName, _bootstrapperModel.ServiceOptions.ConnectionStrings["MongoDB"]));

            return Task.CompletedTask;
        }
    }
}
