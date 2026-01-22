using FluentValidation.Results;
using WizCo.Infrastructure.Services.Interfaces;

namespace WizCo.Infrastructure.Services
{
    public class ServiceBase : IServiceBase
    {
        private readonly IServiceContext _serviceContext;

        public ServiceBase(IServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public bool IsValidOperation => !_serviceContext.HasNotification();

        public IReadOnlyCollection<string> Notification => _serviceContext.Notifications;

        public void AddNotification(string message) => _serviceContext.AddNotification(message);

        public void AddNotifications(ValidationResult validationResult)
        {
            validationResult.Errors.ForEach(error => AddNotification(error.ErrorMessage));
        }

        public bool HasNotification() => _serviceContext.HasNotification();
    }
}
