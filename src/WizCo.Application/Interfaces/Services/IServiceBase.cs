namespace WizCo.Application.Interfaces.Services
{
    public interface IServiceBase
    {
        bool IsValidOperation { get; }

        void AddNotification(string message);
    }
}
