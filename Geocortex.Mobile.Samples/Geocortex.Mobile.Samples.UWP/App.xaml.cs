using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Geocortex.Mobile.Samples.UWP;

// TODO: Platform specific bootstrapping code should be migrated from App.xaml.cs.original to App.xaml.cs or MauiProgram.cs.
// See Windows App Lifecycle: https://learn.microsoft.com/dotnet/maui/fundamentals/app-lifecycle#windows

public partial class App : MauiWinUIApplication
{
    public App()
    {
        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
