using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sample.AttachedProperty
{
    public class SetFontPanel
    {
        #region ChildrenFontSize

        public static readonly DependencyProperty ChildrenFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "ChildrenFontSize",
                typeof(double),
                typeof(SetFontPanel),
                new PropertyMetadata((double)12, OnChildrenFontSizePropertyChanged));

        public static double GetChildrenFontSize(DependencyObject dp)
        {
            return (double)dp.GetValue(ChildrenFontSizeProperty);
        }

        public static void SetChildrenFontSize(DependencyObject dp, double value)
        {
            dp.SetValue(ChildrenFontSizeProperty, value);
        }

        private static void OnChildrenFontSizePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            SetFontProperties(panel);
        }

        #endregion

        #region ChildrenForeground

        public static readonly DependencyProperty ChildrenForegroundProperty =
            DependencyProperty.RegisterAttached(
                "ChildrenForeground",
                typeof(Brush),
                typeof(SetFontPanel),
            new PropertyMetadata(null));

        public static Brush GetChildrenForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ChildrenForegroundProperty);
        }

        public static void SetChildrenForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ChildrenForegroundProperty, value);
        }

        #endregion

        #region Attach

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(bool),
                typeof(SetFontPanel),
            new PropertyMetadata(false, OnAttachPropertyChanged));

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        private static void OnAttachPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            if ((bool)e.NewValue)
            {
                panel.Loaded += panel_Loaded;
            }
            else
            {
                panel.Loaded -= panel_Loaded;
            }
        }

        #endregion

        // FontFamily
        // FontWeight
        // FontStyle
        // FontStretch

        #region Event Handler

        private static void panel_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            SetFontProperties(panel);
        }

        private static void Panel_Unloaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;
            panel.Loaded -= panel_Loaded;
            panel.Unloaded -= Panel_Unloaded;
        }

        #endregion

        #region Method

        private static void SetFontProperties(Panel panel)
        {
            foreach (var child in panel.Children)
            {
                if (child == null) return;

                if (child is TextBlock)
                {
                    var txt = child as TextBlock;
                    if (txt == null) return;

                    txt.FontSize = GetChildrenFontSize(panel);
                    txt.Foreground = GetChildrenForeground(panel);
                    // FontFamily
                    // FontWeight
                    // FontStyle
                    // FontStretch
                }
                else if (child is Control)
                {
                    var ctr = child as Control;
                    if (ctr == null) return;

                    ctr.FontSize = GetChildrenFontSize(panel);
                    ctr.Foreground = GetChildrenForeground(panel);
                    // FontFamily
                    // FontWeight
                    // FontStyle
                    // FontStretch
                }
                //else if (또 다른 컨트롤을 추가)
                //{

                //}
            }
        }

        #endregion
    }
}
