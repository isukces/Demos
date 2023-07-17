using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Extensions = Demo01_AutoRegisterMediatorHandlers.Extensions;

namespace Demo01_AutoRegisterMediatorHandlers.Mediator;

public class MediatorRegistry
{
    public MediatorRegistry(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    static void Helper_AddTransient<TService, TImplementation>(IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        Console.WriteLine("Registering {0} as {1}", 
            typeof(TImplementation).FriendlyName(), 
            typeof(TService).FriendlyName());
        services.AddTransient<TService, TImplementation>();
    }


    private void CallAddTransient(Type a, Type b)
    {
        _helperMethod ??= typeof(MediatorRegistry).GetMethod(nameof(Helper_AddTransient),
            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        if (_helperMethod is null)
            throw new InvalidOperationException("Method not found");
        var c = _helperMethod.MakeGenericMethod(a, b);
        c.Invoke(null, new object[] { _serviceCollection });
    }

    public void ScanAndRegisterHandlers(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsInterface || type.IsAbstract)
                continue;
            var interfaces = type.GetInterfaces();
            foreach (var interfaceType in interfaces)
            {
                if (!interfaceType.IsGenericType) continue;
                var d = interfaceType.GetGenericTypeDefinition();
                if (d != typeof(ISimpleRequestHandler<,>)) continue;
                CallAddTransient(interfaceType, type);
            }
        }
    }

    public void ScanAndRegisterHandlers(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            ScanAndRegisterHandlers(assembly);
        }
    }

    private readonly IServiceCollection _serviceCollection;

    private MethodInfo? _helperMethod;
}
