using Demo01_AutoRegisterMediatorHandlers;
using Demo01_AutoRegisterMediatorHandlers.Mediator;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello - autorejestracja handlerów mediatora");

var services = Prepare();
var mediator = services.GetRequiredService<ISimpleMediator>();

var request = new RectangleAreaRequest { Width = 2, Height = 3 };
var result  = mediator.Send(request);
Console.WriteLine($"Prostokąt {request.Width}x{request.Height} ma pole {result.Area}");

// ================== INICJALIZACJA ==================

static IServiceProvider Prepare()
{
    IServiceCollection serviceCollection = new ServiceCollection();
    serviceCollection.AddSingleton<ISimpleMediator>(a => new SimpleMediator(a));
    new MediatorRegistry(serviceCollection)
        .ScanAndRegisterHandlers(typeof(Program).Assembly);
    return serviceCollection.BuildServiceProvider();
}
