# Kaskadowe kalkulacje

Projekt zawiera prostą implementację obliczeń w kaskadowo powiązanych ze sobą kalkulatorach (handlerach).

Poszczególne foldery zawierają
* `_architecture` klasy odpowiadające za zbudowanie otoczenia umożliwiającego działanie kalkulatorów
* `_business` dokument i sesja obliczania dokumentu
* `_business\_calculators` przykładowe kalkulatory.
* `_business\_session` kontekst potrzebny do obliczeń np. dostęp do bazy pomiarów, harmonogram zmiany cen itp
* `_business\_interfaces` klasy/interfejsy domenowe np. `IEnergyDataProvider`

Przykład obejmuje zagadnienia

* Sekwencyje uruchamianie kalkulatorów w zestawie obliczeniowym `SampleCalculationSet`
* obsługa `RecomputeAgainException` - następuje w przypadku, kiedy w trakcie obliczeń okazuje się, że należy zmodyfikować założenia pierwotne i powtórzyć obliczenia od początu. Tutaj zasymulowana jest zmiana ceny w trakcie okresu obliczeniowego
* keszowanie dostępu do danych `CachedEnergyPriceProvider` oraz `CachedPowerConsumptionDataProvider`


## Linki

* [(167) Master the Power of Composite Pattern in Rich Domain Modeling - YouTube.url](https://www.youtube.com/watch?v=1l_hHoMgTV0)
* [Wzorzec Chain-of-responsibility](https://pl.wikipedia.org/wiki/%C5%81a%C5%84cuch_zobowi%C4%85za%C5%84)

