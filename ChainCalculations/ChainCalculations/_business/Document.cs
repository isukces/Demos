namespace ChainCalculations;

/// <summary>
///     Dokument będący wynikiem obliczeń
/// </summary>
public class Document
{
    public void Print()
    {
        Console.WriteLine($"Faktura {Number} z dnia {CreateDate}");
        Console.WriteLine($"Odbiorca {Buyer}");
        for (var index = 0; index < Items.Count; index++)
        {
            var item = Items[index];
            Console.WriteLine((index + 1) + "." + item);
        }
    }

    public DateOnly CreateDate { get; set; }
    public string   Number     { get; set; }

    public List<DocumentItem> Items { get; } = new();
    public string             Buyer { get; set; }
}

public record DocumentItem(string Name, decimal Price, int Quantity, decimal TaxRate,
    decimal TotalPrice);
