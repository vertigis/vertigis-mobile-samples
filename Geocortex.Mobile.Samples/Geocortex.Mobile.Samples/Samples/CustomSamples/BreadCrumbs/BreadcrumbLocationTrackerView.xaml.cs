using VertiGIS.Mobile.Composition;
using VertiGIS.Mobile.Samples.Samples.CustomSamples.Breadcrumbs;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

[assembly: View(typeof(BreadcrumbLocationTrackerView))]
namespace VertiGIS.Mobile.Samples.Samples.CustomSamples.Breadcrumbs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BreadcrumbLocationTrackerView : ContentView
    {
        public BreadcrumbLocationTrackerViewModel ViewModel => (BreadcrumbLocationTrackerViewModel)BindingContext;

        public BreadcrumbLocationTrackerView(BreadcrumbLocationTrackerViewModel viewModel)
		{
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}
