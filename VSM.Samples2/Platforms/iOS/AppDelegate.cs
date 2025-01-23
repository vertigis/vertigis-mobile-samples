using Foundation;
using UIKit;
using VertiGIS.Mobile.Platforms;
using VSM.Samples2;

namespace VertiGIS.Mobile.Samples;

// TODO: Platform specific bootstrapping code should be migrated from AppDelegate.cs.original to AppDelegate.cs or MauiProgram.cs.
// See iOS App Lifecycle: https://learn.microsoft.com/dotnet/maui/fundamentals/app-lifecycle#ios

[Register(nameof(AppDelegate))]
public class AppDelegate : VertiGISAppDelegate
{
    //
    // This method is invoked when the application has loaded and is ready to run. In this 
    // method you should instantiate the window, load the UI into it and then make the window
    // visible.
    //
    // You have 17 seconds to return from this method, or iOS will terminate your application.
    //
    public override bool FinishedLaunching(UIApplication app, NSDictionary options)
    {
        return base.FinishedLaunching(app, options);
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
