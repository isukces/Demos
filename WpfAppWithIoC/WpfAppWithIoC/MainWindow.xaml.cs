using System.Windows;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppWithIoC;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnMakeInvoice_OnClick(object sender, RoutedEventArgs e)
    {
        // KROK 3: Żądanie dostarczenia obiektu 
        var invoiceMaker = AppGlobal.Services.GetRequiredService<IInvoiceMaker>();
        // KROK 4: Wywołanie metody biznesowej
        invoiceMaker.MakeInvoice();

    }
}