using MediatR;

namespace ITnCare.Sample
{
    public class TestNotification : INotification
    {
        public string Message { get; set; } = default!;
    }
}
