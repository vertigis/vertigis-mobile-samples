using VertiGIS.Mobile.Samples.Location;
using Android.Content;
using Android.Locations;
using AndroidApplication = Android.App.Application;
using ALLocation = Android.Locations.Location;
using AOSBundle = Android.OS.Bundle;

/* NOTE: This sample component is for demonstrative purposes only.
 * This is not the recommended pattern for accessing location in a VertiGIS Studio Mobile application.
 * This component is used to demonstrate platform specific implementations and api/method calls. */
[assembly: Microsoft.Maui.Controls.Dependency(typeof(AndroidLocation))]
namespace VertiGIS.Mobile.Samples.Location
{
    public class AndroidLocation : Java.Lang.Object, ILocation, ILocationListener
    {
        private LocationManager _locationManager;

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, AOSBundle extras) { }

        public void OnLocationChanged(ALLocation location)
        {
            if (location != null)
            {
                LocationEventArgs args = new LocationEventArgs
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };
                LocationObtained(this, args);
            };
        }

        public event EventHandler<ILocationEventArgs> LocationObtained;

        event EventHandler<ILocationEventArgs> ILocation.LocationObtained
        {
            add
            {
                LocationObtained += value;
            }
            remove
            {
                LocationObtained -= value;
            }
        }

        public void ObtainMyLocation()
        {
            _locationManager = (LocationManager)AndroidApplication.Context.GetSystemService(Context.LocationService);
            _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 0, 0, this);
        }

        // Unsubscribe
        ~AndroidLocation()
        {
            _locationManager.RemoveUpdates(this);
        }
    }

    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
