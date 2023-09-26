using System;
using Common;
using Microsoft.Extensions.DependencyInjection;
using SomeVisualLib.InvoiceMaker;

namespace WpfAppWithIoC;

public static class ApplicationStartup
{
    public static IServiceProvider ConfigureServices()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        // KROK 2: To jest jedyne miejsce gdzie znana jest implementacja InvoiceMakerImplementation
        serviceCollection.AddSingleton<IInvoiceMaker, InvoiceMakerImplementation>();
        return serviceCollection.BuildServiceProvider();

    }
}
