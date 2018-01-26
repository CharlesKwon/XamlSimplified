using System.Windows;
using System.Windows.Controls;

namespace Sample.Control
{
    public class TwoContentsButton : Button
    {
        #region SecondContent : 두번째 Content

        public string SecondContent
        {
            get { return (string)GetValue(SecondContentProperty); }
            set { SetValue(SecondContentProperty, value); }
        }

        public static readonly DependencyProperty SecondContentProperty =
            DependencyProperty.Register(
                "SecondContent", 
                typeof(string), 
                typeof(TwoContentsButton), 
                new PropertyMetadata(string.Empty));

        #endregion
    }
}
