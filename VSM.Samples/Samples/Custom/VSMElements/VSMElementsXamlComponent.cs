using VertiGIS.Mobile.Samples;
using VertiGIS.Mobile.Samples.Samples.Custom.VSMElements;
using VertiGIS.Mobile.Composition.Layout;
using System.Xml.Linq;

[assembly: Component(typeof(VSMElementsComponent), "vsm-elements-xaml", XmlNamespace = XmlNamespaces.SamplesNamespace)]
namespace VertiGIS.Mobile.Samples.Samples.Custom.VSMElements
{
    internal class VSMElementsXamlComponent : ComponentBase
    {
        private VSMElementsXamlView _view;

        public VSMElementsXamlComponent()
        {
        }

        protected override VisualElement Create(XNode node)
        {
            _view = new VSMElementsXamlView();
            return _view;
        }
    }
}
