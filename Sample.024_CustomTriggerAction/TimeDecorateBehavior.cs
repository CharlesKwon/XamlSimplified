using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Sample.Behavior
{
    public class TextChangedTrigger : TriggerBase<TextBlock>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            var textBlock = AssociatedObject as TextBlock;
            if (textBlock == null) return;

            var dpd = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));
            if (dpd == null) return;
            dpd.AddValueChanged(textBlock, OnTextChanged);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            InvokeActions(sender);
        }
    }

    public class AddZeroAction : TriggerAction<TextBlock>
    {
        protected override void Invoke(object parameter)
        {
            var textBlock = parameter as TextBlock;
            if (textBlock == null) return;
            if (textBlock.Text.Length == 1)
            {
                textBlock.Text = "0" + textBlock.Text;
            }
        }
    }
}
