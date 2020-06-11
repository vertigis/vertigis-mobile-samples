﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CarouselView.FormsPlugin.iOS;
using FFImageLoading.Forms.Platform;
using Foundation;
using VertiGIS.Mobile;
using VertiGIS.Mobile.Composition.Logging;
using VertiGIS.Mobile.Infrastructure.App;
using VertiGIS.Mobile.Infrastructure.Configuration;
using VertiGIS.Mobile.Infrastructure.Internationalization;
using VertiGIS.Mobile.Infrastructure.Platform;
using VertiGIS.Mobile.Platform;
using VertiGIS.Mobile.Utilities;
using Geocortex.Workflow.Forms.IOS;
using Rg.Plugins.Popup;
using UIKit;
using Xamarin.Forms;

namespace VertiGIS.Mobile.Samples.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : VertiGISAppDelegate
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
            IOSInitializer.Init();
            Forms.Init();
            var viewerApp = new App();
            LoadApplication(viewerApp);

            return base.FinishedLaunching(app, options);
        }
    }
}
