using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Sample.Behavior
{
    public class OnlyOneExpanderBehavior : Behavior<Panel>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;

            base.OnDetaching();
        }
        
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var child in AssociatedObject.Children)
            {
                if (child == null) return;
                var allExpander = child as Expander;
                if (allExpander != null)
                {
                    allExpander.Expanded += Expander_Expanded;
                }
            }
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            foreach (var child in AssociatedObject.Children)
            {
                if (child == null) return;
                var allExpander = child as Expander;
                if (allExpander != null)
                {
                    allExpander.IsExpanded = false;
                }
            }

            var expander = sender as Expander;
            if (expander == null) return;
            expander.Expanded -= Expander_Expanded;
            expander.IsExpanded = true;
            expander.Expanded += Expander_Expanded;
        }
    }
}
