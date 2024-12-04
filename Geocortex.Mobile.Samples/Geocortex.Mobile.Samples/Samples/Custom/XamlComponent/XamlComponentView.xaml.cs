using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace VertiGIS.Mobile.Samples.Samples.Custom.XamlComponent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamlComponentView : ContentView
    {
        public XamlComponentView()
        {
            InitializeComponent();
        }
    }
}
