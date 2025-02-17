﻿using Esri.ArcGISRuntime.Mapping;
using VertiGIS.Mobile.Composition;
using VertiGIS.Mobile.Composition.Messaging;
using VertiGIS.Mobile.Composition.Services;
using VertiGIS.Mobile.Samples.Samples.CustomSamples.BasemapToggle;

[assembly: Service(typeof(BasemapToggleService), PropertiesAutowired = true)]
namespace VertiGIS.Mobile.Samples.Samples.CustomSamples.BasemapToggle
{
    public class BasemapToggleService : ServiceBase
    {
        public BasemapOperations BasemapOperations { get; set; }

        protected override Task DoInitialize()
        {
            this.BasemapOperations.ToggleDisplay.RegisterExecute(
                (args) =>
                {
                    foreach (Layer basemapLayer in args.Map.Map.Basemap.BaseLayers)
                    {
                        basemapLayer.IsVisible = args.ToggleOn;
                    }

                    return Task.FromResult((object)null);
                }, this);

            this.BasemapOperations.ToggleBasemap.RegisterExecute(
                (args) =>
                {
                    if (args.ToggleOn)
                    {
                        args.Map.Map.Basemap = new Basemap(BasemapStyle.ArcGISNavigation);

                    }
                    else
                    {
                        args.Map.Map.Basemap = new Basemap(BasemapStyle.ArcGISStreetsNight);
                    }

                    return Task.FromResult((object)null);
                }, this);

            return Task.CompletedTask;
        }
    }
}
