using ITnCare.Service.Models;

namespace ITnCare.Service.Bootstrapper
{
    internal class KafkaBootstrapper
    {
        private readonly BootstrapperModel _bootstrapperModel;

        public KafkaBootstrapper(BootstrapperModel bootstrapperModel)
        {
            _bootstrapperModel = bootstrapperModel;
        }

        internal Task Bootstrap()
        {
            return Task.CompletedTask;
        }
    }
}
