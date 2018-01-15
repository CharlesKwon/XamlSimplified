using System.Windows;
using System.Windows.Controls;

namespace Sample.Control
{
    public class GuideTextBox:TextBox
    {
        #region GuideText : 가이드 문구

        public object GuideText
        {
            get { return (object)GetValue(GuideTextProperty); }
            set { SetValue(GuideTextProperty, value); }
        }

        public static readonly DependencyProperty GuideTextProperty =
            DependencyProperty.Register("GuideText", 
                typeof(object), 
                typeof(GuideTextBox), 
                new PropertyMetadata("Guide Text"));

        #endregion

        #region HasGuideText : 사용자 입력 텍스트 유무

        public bool HasInputText
        {
            get { return (bool)GetValue(HasInputTextProperty); }
            set { SetValue(HasInputTextProperty, value); }
        }

        public static readonly DependencyProperty HasInputTextProperty =
            DependencyProperty.Register("HasInputText", 
                typeof(bool), 
                typeof(GuideTextBox), 
                new PropertyMetadata(false));

        #endregion

        public GuideTextBox()
        {
            this.TextChanged += GuideTextBox_TextChanged;
            this.Unloaded += GuideTextBox_Unloaded;
        }

        private void GuideTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                HasInputText = false;
            }
            else
            {
                HasInputText = true;
            }
        }

        private void GuideTextBox_Unloaded(object sender, RoutedEventArgs e)
        {
            this.TextChanged -= GuideTextBox_TextChanged;
            this.Unloaded -= GuideTextBox_Unloaded;
        }
    }
}
