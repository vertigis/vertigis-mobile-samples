﻿using Esri.ArcGISRuntime.Maui;
using VertiGIS.Mobile.Composition;
using VertiGIS.Mobile.Composition.Layout;
using VertiGIS.Mobile.Samples.Samples.CustomSamples.BasemapToggle;

[assembly: View(typeof(BasemapToggleView))]
namespace VertiGIS.Mobile.Samples.Samples.CustomSamples.BasemapToggle
{
    public partial class BasemapToggleView : ContentView
    {
        private readonly BasemapOperations _basemapOperations;
        private readonly ILayoutModel<MapView> _mapView;
        private bool mapSwitch = true;

        public BasemapToggleView(BasemapOperations basemapOperations, ILayoutModel<MapView> mapView)
        {
            _basemapOperations = basemapOperations;
            _mapView = mapView;
            InitializeComponent();
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            bool toggleOn = e.Value;
            MapView mapView = await _mapView.ResolveAsync();
            await _basemapOperations.ToggleDisplay.ExecuteAsync(new BasemapToggleCommandArgs(mapView, toggleOn));
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            MapView mapView = await _mapView.ResolveAsync();
            mapSwitch = !mapSwitch;
            await _basemapOperations.ToggleBasemap.ExecuteAsync(new BasemapToggleCommandArgs(mapView, mapSwitch));
        }
    }
}
