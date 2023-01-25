using ITnCare.Service.Models;
using MediatR;

namespace ITnCare.Service.Bootstrapper
{
    internal class MediatRBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        internal MediatRBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap<T>() where T : class
        {
            _bootstrapperModel.Services.AddMediatR(typeof(T).Assembly);
            return Task.CompletedTask;
        }
    }
}
