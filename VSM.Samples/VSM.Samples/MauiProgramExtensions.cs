using CommunityToolkit.Maui;
using Esri.ArcGISRuntime.Maui;
using Esri.ArcGISRuntime.Toolkit.Maui;
using VertiGIS.Mobile.Platform;

namespace VertiGIS.Mobile.Samples;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseStudioMobile()
            .UseArcGISRuntime()
            .UseArcGISToolkit()
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>();

        // TODO: Add the entry points to your Apps here.
        // See also: https://learn.microsoft.com/dotnet/maui/fundamentals/app-lifecycle
        //builder.Services.AddTransient<AppShell, AppShell>();


        return builder;
    }
}
