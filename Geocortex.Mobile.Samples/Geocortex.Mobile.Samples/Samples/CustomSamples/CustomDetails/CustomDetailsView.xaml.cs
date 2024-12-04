using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace VertiGIS.Mobile.Samples.Samples.CustomSamples.CustomDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDetailsView : ContentView
    {
        public CustomDetailsView()
        {
            InitializeComponent();
        }

        public void SetSource(string source)
        {
            SourceLabel.Text = source;
        }

        public void SetTitle(string title)
        {
            TitleLabel.Text = title;
        }
    }
}
