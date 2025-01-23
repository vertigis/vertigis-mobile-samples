using Android.App;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using VertiGIS.Mobile.Platforms;
using static Java.Lang.Thread;

namespace VertiGIS.Mobile.Samples;

// TODO: The original platform specific bootstrapping code has been archived as MainActivity.cs.original. ActivityAttributes should be migrated from the archived file to this one and any additional changes need to be manually migrated to MauiProgram.cs
// See Android App Lifecycle: https://learn.microsoft.com/dotnet/maui/fundamentals/app-lifecycle#android
// See MainActivity: https://learn.microsoft.com/dotnet/maui/android/manifest#activity-name

[Activity(Name = "samples.mainactivity",
          Theme = "@style/Maui.SplashTheme",
          MainLauncher = true,
          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : VertiGISMobileActivity, IUncaughtExceptionHandler
{
    protected override void OnCreate(Bundle bundle)
    {
        DefaultUncaughtExceptionHandler = this;
        HandleExceptions();

        // The app was launched with the splash screen theme, so change it to the main theme now
        base.SetTheme(Resource.Style.MainTheme);

        base.OnCreate(bundle);

        // Handle startup urls
        HandleOnCreateIntent(); // Startup urls
        HandleFullyDrawn(); // Android diagnostics
        HandleAppPermissions(); // Location, bluetooth, etc.
    }
    public void UncaughtException(Java.Lang.Thread t, Throwable e)
    {
    }
}
