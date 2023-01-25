using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace ITnCare.Service
{
    public class DebugWorker : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartDebugger(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private Task StartDebugger(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                int counter = 1;
                do
                {
                    Debug.WriteLine($"Debugger is working in the background. Counter: {counter++}");
                    await Task.Delay(TimeSpan.FromSeconds(2));
                } while (!cancellationToken.IsCancellationRequested);
            });

            return Task.CompletedTask;
        }
    }
}
