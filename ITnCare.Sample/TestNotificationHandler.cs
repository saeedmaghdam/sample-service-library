using MediatR;

namespace ITnCare.Sample
{
    public class TestNotificationHandler : INotificationHandler<TestNotification>
    {
        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine(notification.Message);
            return Task.CompletedTask;
        }
    }
}
