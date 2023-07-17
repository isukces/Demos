# Proste demo mediatora

Program przedstawia prosty przykład użycia mediatora. 

Mediator stanowi rejestr handlerów, które są wywoływane w odpowiedzi na zapytania.
Każdy handler musi implementować interfejs `ISimpleRequestHandler<,>`. Zawiera on jedną metodę `Handle` przyjmującą zapytanie i zwracającą odpowiedź.

W podanym rejestracja handlerów odbywa się ręcznie poprzez:

```cs
serviceCollection.AddTransient<ISimpleRequestHandler<RectangleAreaRequest, AreaResponse>, RectangleAreaHandler>();
```

Żądanie (w tym przykładzie obliczenie pola powierzchni) zostaje przesłane do mediatora poprzez metodę `Send`:

```cs
var request = new RectangleAreaRequest { Width = 2, Height = 3 };
var result  = mediator.Send(request);
```

Mediator na podstawie typu żądania odszukuje stosowny handler i przekazuje mu żądanie. Handler zwraca odpowiedź, która jest następnie zwracana przez mediator.

