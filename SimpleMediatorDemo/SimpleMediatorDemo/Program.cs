using Microsoft.Extensions.DependencyInjection;
using SimpleMediatorDemo;
using SimpleMediatorDemo.Mediator;

Console.WriteLine("Hello, World!");

var services = Prepare();
var mediator = services.GetRequiredService<ISimpleMediator>();

var request = new RectangleAreaRequest { Width = 2, Height = 3 };
var result  = mediator.Send(request);
Console.WriteLine($"Area: {result.Area}");

// ================== INICJALIZACJA ==================

static IServiceProvider Prepare()
{
    IServiceCollection serviceCollection = new ServiceCollection();
    serviceCollection.AddSingleton<ISimpleMediator>(a => new SimpleMediator(a));
    serviceCollection.AddTransient<ISimpleRequestHandler<RectangleAreaRequest, AreaResponse>, RectangleAreaHandler>();
    return serviceCollection.BuildServiceProvider();
}
