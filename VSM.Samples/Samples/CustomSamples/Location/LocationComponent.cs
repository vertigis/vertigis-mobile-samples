﻿using VertiGIS.Mobile.Samples.Samples.CustomSamples.Location;
using VertiGIS.Mobile.Samples;
using VertiGIS.Mobile.Samples.Location;
using VertiGIS.Mobile.Composition.Layout;
using System.Xml.Linq;

/* NOTE: This sample component is for demonstrative purposes only.
 * This is not the recommended pattern for accessing location in a VertiGIS Studio Mobile application.
 * This component is used to demonstrate platform specific implementations and api/method calls. */
[assembly: Component(typeof(LocationComponent), "location", XmlNamespace = XmlNamespaces.SamplesNamespace)]
namespace VertiGIS.Mobile.Samples.Samples.CustomSamples.Location
{
    internal class LocationComponent : ComponentBase
    {
        private StackLayout _stack;
        private Label _latitude;
        private Label _longitude;
        ILocation _location;

        public LocationComponent()
        {
        }

        protected override VisualElement Create(XNode node)
        {
            _stack = new StackLayout();

            _latitude = new Label()
            {
                Margin = 10,
                HorizontalTextAlignment = TextAlignment.Center
            };

            _longitude = new Label()
            {
                Margin = 10,
                HorizontalTextAlignment = TextAlignment.Center
            };

            _stack.Children.Add(_latitude);
            _stack.Children.Add(_longitude);

            return _stack;
        }

        protected override Task DoInitializeAsync()
        {

            _latitude.Text = $"Latitude: ";
            _longitude.Text = $"Longitude: ";

            // Get our platform specific location implementation.
            _location = DependencyService.Get<ILocation>();

            // Add our event handler - updating lat/long.
            _location.LocationObtained += (object sender,
                ILocationEventArgs e) =>
            {
                var lat = e.Latitude;
                var lng = e.Longitude;

                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    _latitude.Text = $"Latitude: {lat.ToString()}";
                    _longitude.Text = $"Longitude: {lng.ToString()}";
                });
            };

            _location.ObtainMyLocation();

            return Task.CompletedTask;
        }
    }
}
