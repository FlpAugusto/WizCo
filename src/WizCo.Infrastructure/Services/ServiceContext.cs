using System;
using System.Collections.Generic;
using System.Linq;
using WizCo.Infrastructure.Services.Interfaces;

namespace Shared.Infrastructure.Services;

public class ServiceContext : IServiceContext
{
    private readonly List<string> _notifications;

    public ServiceContext()
    {
        _notifications = new List<string>();
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

    public void ClearNotification()
    {
        _notifications.Clear();
    }

    public bool HasNotification() => Notifications.Any();
}
