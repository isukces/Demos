namespace Demo01_AutoRegisterMediatorHandlers;

public interface ISimpleRequestHandler<in TRequest, out TResponse>
    where TRequest : ISimpleRequest<TResponse>
{
    /// <summary>Handles a request</summary>
    /// <param name="request">The request</param>
    /// <returns>Response from the request</returns>
    TResponse Handle(TRequest request);
}
