# Autorejestracja handlerów dla mediatora

Rejestracja polega na przeskanowaniu assembly w poszukiwaniu klas implementujących interfejs `ISimpleRequestHandler<,>`. Po znalezieniu klasy w kontenerze IoC dokonywana jest rejestracja klasy jako implementacji.

Uruchomienie automatycznego skanowania odbywa się poprzez

```cs
new MediatorRegistry(serviceCollection)
    .ScanAndRegisterHandlers(typeof(Program).Assembly);
```
