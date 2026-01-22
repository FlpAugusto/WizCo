namespace WizCo.Application.Shared.Results;

public class ApiBadRequestResult
{
    public IReadOnlyCollection<string> Notifications { get; }

    public ApiBadRequestResult(IReadOnlyCollection<string> notifications)
    {
        Notifications = notifications;
    }
}
