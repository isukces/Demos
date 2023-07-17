namespace Demo01_AutoRegisterMediatorHandlers;

public interface IRegistrationInfo
{
    object HandleRequest(IServiceProvider services, object request);
}
