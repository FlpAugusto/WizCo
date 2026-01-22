namespace WizCo.Infrastructure.Services.Interfaces
{
    public interface IServiceBase
    {
        bool IsValidOperation { get; }

        void AddNotification(string message);
    }
}
