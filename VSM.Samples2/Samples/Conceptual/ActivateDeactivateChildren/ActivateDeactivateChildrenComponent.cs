using System.Xml.Linq;
using VertiGIS.Mobile.Composition.Layout;
using VertiGIS.Mobile.Samples;
using VertiGIS.Mobile.Samples.Samples.Conceptual.ActivateDeactivateChildren;

[assembly: Component(typeof(ActivateDeactivateChildrenComponent), "activate-deactivate-children", XmlNamespace = XmlNamespaces.SamplesNamespace)]
namespace VertiGIS.Mobile.Samples.Samples.Conceptual.ActivateDeactivateChildren
{
    internal class ActivateDeactivateChildrenComponent : ComponentBase
    {
        private Grid _grid;

        public ActivateDeactivateChildrenComponent() : base()
        {
        }

        protected override VisualElement Create(XNode node)
        {
            _grid = new Grid();

            int i = 0;

            _grid.RowDefinitions = new RowDefinitionCollection { new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } };
            _grid.ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) } };

            foreach (var child in Children)
            {
                var view = (View)child.GetView();

                _grid.Children.Add(view);
                Grid.SetRow(view, 0);
                Grid.SetColumn(view, i);

                _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                i++;
            }

            return _grid;
        }

        protected override Task DeactivateChildAsync(ComponentBase removed)
        {
            // Remove the child.
            var row = Grid.GetRow(removed.GetView());
            _grid.Children.Remove((View)removed.GetView());

            if (_grid.Children.Count == 0)
            {
                return Task.CompletedTask;
            }

            // Move all subsequent children up a row.
            foreach (var child in Children.Where(child => Grid.GetRow(child.GetView()) > row))
            {
                Grid.SetRow(child.GetView(), Grid.GetRow(child.GetView()) - 1);
            }

            return Task.CompletedTask;
        }

        protected override Task ActivateChildAsync(ComponentBase child)
        {
            // Add children to the bottom of the list.
            var view = (View)child.GetView();

            _grid.Children.Add(view);

            if (_grid.RowDefinitions.Count < _grid.Children.Count)
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            Grid.SetRow(view, _grid.Children.Count - 1);

            return Task.CompletedTask;
        }
    }
}
