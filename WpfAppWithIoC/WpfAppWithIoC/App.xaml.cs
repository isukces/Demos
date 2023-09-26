using System.Windows;

namespace WpfAppWithIoC;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        // KROK 1: Zarejestrowanie usług
        AppGlobal.Services = ApplicationStartup.ConfigureServices();
    }
}