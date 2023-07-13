namespace SimpleMediatorDemo;

public interface IRegistrationInfo
{
    object HandleRequest(IServiceProvider services, object request);
}
