using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Sample.Behavior
{
    public class TimeChangedTrigger : TriggerBase<TextBlock>
    {
        public string SpecialTime
        {
            get { return (string)GetValue(SpecialTimeProperty); }
            set { SetValue(SpecialTimeProperty, value); }
        }

        public static readonly DependencyProperty SpecialTimeProperty =
            DependencyProperty.Register(
                "SpecialTime", 
                typeof(string), 
                typeof(TimeChangedTrigger), 
                new PropertyMetadata(string.Empty));

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
            var textBlock = AssociatedObject as TextBlock;
            if (textBlock == null) return;

            var dpd = DependencyPropertyDescriptor.FromProperty(TextBlock.TextProperty, typeof(TextBlock));
            if (dpd == null) return;
            dpd.AddValueChanged(textBlock, OnTextChanged);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var textBlock = sender as TextBlock;
            if (textBlock == null) return;

            if (textBlock.Text == SpecialTime)
            {
                InvokeActions("ALARM");
            }
            else
            {
                InvokeActions("");
            }
        }
    }

    public class ChangeForegroundAction : TriggerAction<TextBlock>
    {
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                "Foreground", 
                typeof(Brush), 
                typeof(ChangeForegroundAction), 
                new PropertyMetadata(Brushes.Black));

        protected override void Invoke(object parameter)
        {
            var textBlock = AssociatedObject as TextBlock;
            if (textBlock == null || parameter == null) return;

            var param = parameter.ToString().ToUpper();
            if (param == "ALARM")
            {
                textBlock.Foreground = Foreground;
            }
            else 
            {
                textBlock.Foreground = Brushes.Black;
            }
        }
    }
}
