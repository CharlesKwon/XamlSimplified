using System.Windows;

namespace Sample.AttachedProperty
{
    public class AP_TwoContentsButton
    {
        #region SecondContent : 두번째 Content

        public static readonly DependencyProperty SecondContentProperty =
            DependencyProperty.RegisterAttached(
                "SecondContent",
                typeof(string),
                typeof(AP_TwoContentsButton),
                new PropertyMetadata(string.Empty));

        public static string GetSecondContent(DependencyObject dp)
        {
            return (string)dp.GetValue(SecondContentProperty);
        }

        public static void SetSecondContent(DependencyObject dp, string value)
        {
            dp.SetValue(SecondContentProperty, value);
        }

        #endregion
    }
}
