﻿using Microsoft.Extensions.DependencyInjection;
using SimpleMediatorDemo;
using SimpleMediatorDemo.Mediator;

Console.WriteLine("Hello - proste demo idei działania mediatora");

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
    serviceCollection.AddTransient<ISimpleRequestHandler<RectangleAreaRequest, AreaResponse>, RectangleAreaHandler>();
    return serviceCollection.BuildServiceProvider();
}
