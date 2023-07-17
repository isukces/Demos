namespace Demo01_AutoRegisterMediatorHandlers;

public interface ISimpleMediator
{
    /// <summary>
    ///     Send a request to a single handler
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    /// <param name="request">Request object</param>
    /// <returns>A task that represents the send operation. The task result contains the handler response</returns>
    TResponse Send<TResponse>(ISimpleRequest<TResponse> request);
}
