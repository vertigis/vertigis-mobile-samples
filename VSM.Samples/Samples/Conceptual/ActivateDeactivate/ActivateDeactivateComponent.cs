﻿using VertiGIS.Mobile.Samples;
using VertiGIS.Mobile.Samples.Samples.Conceptual.ActivateDeactivate;
using VertiGIS.Mobile.Composition.Layout;
using System.Xml.Linq;

[assembly: Component(typeof(ActivateDeactivateComponent), "activate-deactivate", XmlNamespace = XmlNamespaces.SamplesNamespace)]
namespace VertiGIS.Mobile.Samples.Samples.Conceptual.ActivateDeactivate
{
    internal class ActivateDeactivateComponent : ComponentBase
    {
        private Label _label;

        public ActivateDeactivateComponent()
        {
        }

        protected override VisualElement Create(XNode node)
        {
            _label = new Label
            {
                Text = "This component is active.",
                Margin = 5,
                HorizontalTextAlignment = TextAlignment.Center
            };

            return _label;
        }

        protected override async Task ActivatedAsync()
        {
            await Application.Current.MainPage.DisplayAlert("Activated Alert", "This alert is shown when the component is activated.", "OK");
        }

        protected override async Task DeactivatedAsync()
        {
            await Application.Current.MainPage.DisplayAlert("Deactivated Alert", "This alert is shown when the component is deactivated.", "OK");
        }
    }
}
