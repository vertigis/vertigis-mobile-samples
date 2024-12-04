using VertiGIS.Mobile.Composition.Layout;
using VertiGIS.Mobile.Samples;
using VertiGIS.Mobile.Samples.Samples.CustomSamples.Breadcrumbs;
using System.Xml.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

[assembly: Component(typeof(BreadcrumbLocationTrackerComponent), "breadcrumb-location-tracker", XmlNamespace = XmlNamespaces.SamplesNamespace)]
namespace VertiGIS.Mobile.Samples.Samples.CustomSamples.Breadcrumbs
{
    public class BreadcrumbLocationTrackerComponent : ComponentBase
    {
        private BreadcrumbLocationTrackerView _breadcrumbLocationTrackerView;

        public BreadcrumbLocationTrackerComponent(BreadcrumbLocationTrackerView breadcrumbLocationTrackerView)
        {
            this._breadcrumbLocationTrackerView = breadcrumbLocationTrackerView;
        }

        protected override VisualElement Create(XNode node)
        {
            return this._breadcrumbLocationTrackerView;
        }
    }
}
