namespace Demo01_AutoRegisterMediatorHandlers;

/// <summary>
///     Marker interface to represent a request with a response
/// </summary>
/// <typeparam name="TResponse">Response type</typeparam>
public interface ISimpleRequest<out TResponse>
{
}
