using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleMediatorDemo.Mediator;

public sealed class SimpleMediator : ISimpleMediator
{
    public SimpleMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }


    public TResponse Send<TResponse>(ISimpleRequest<TResponse> request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();

        var service = _requestHandlers.GetOrAdd(requestType, t1 =>
        {
            var tt       = typeof(RegistrationInfoGeneric<,>).MakeGenericType(t1, typeof(TResponse));
            var instance = Activator.CreateInstance(tt);
            return (IRegistrationInfo)instance!;
        });

        var response = service.HandleRequest(_serviceProvider, request);
        return (TResponse)response;
    }

    #region Fields

    private readonly IServiceProvider _serviceProvider;

    private readonly ConcurrentDictionary<Type, IRegistrationInfo> _requestHandlers =
        new ConcurrentDictionary<Type, IRegistrationInfo>();

    #endregion

    private sealed class RegistrationInfoGeneric<TRequest, TResponse> : IRegistrationInfo
        where TRequest : ISimpleRequest<TResponse>
    {
        public object HandleRequest(IServiceProvider services, object request)
        {
            var handler       = services.GetRequiredService<ISimpleRequestHandler<TRequest, TResponse>>();
            var requestCasted = (TRequest)request;
            var response      = handler.Handle(requestCasted);
            return response!;
        }
    }
}
