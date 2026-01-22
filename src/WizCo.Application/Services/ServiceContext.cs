using WizCo.Application.Interfaces.Services;

namespace WizCo.Application.Services
{
    public class ServiceContext : IServiceContext
    {
        private readonly List<string> _notifications;

        public ServiceContext()
        {
            _notifications = [];
        }

        public IReadOnlyCollection<string> Notifications { get { return _notifications.AsReadOnly(); } }

        public void AddNotification(string message)
        {
            if (!Notifications.Contains(message))
                _notifications.Add(message);
        }

        public void AddNotifications(IEnumerable<string> messages)
        {
            _notifications.AddRange(messages);
        }

        public bool HasNotification() => Notifications.Any();
    }
}
