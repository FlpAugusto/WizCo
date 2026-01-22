namespace WizCo.Infrastructure.Services.Interfaces
{
    public interface IServiceContext
    {
        IReadOnlyCollection<string> Notifications { get; }

        bool HasNotification();

        void AddNotification(string message);

        void AddNotifications(IEnumerable<string> messages);
    }
}
