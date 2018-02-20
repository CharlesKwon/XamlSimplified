using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Sample.Behavior
{
    public class ToolTipToPopupSliderBehavior : Behavior<Slider>
    {
        #region Variable

        private Thumb _thumb;
        private Popup _popup;
        private double _changedHorizontalOffset;

        #endregion

        #region VerticalOffset : 팝업 세로 위치

        public int VerticalOffset
        {
            get { return (int)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register(
                "VerticalOffset", 
                typeof(int), 
                typeof(ToolTipToPopupSliderBehavior), 
                new PropertyMetadata(0));

        #endregion

        #region Proteced method 

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            _thumb.DragDelta -= Thumb_DragDelta;
            _thumb.DragStarted -= Thumb_DragStarted;
            _thumb.DragCompleted -= Thumb_DragCompleted;

            AssociatedObject.PreviewKeyDown -= Slider_PreviewKeyDown;
            AssociatedObject.KeyUp -= Slider_KeyUp;

            _thumb = null;
            _popup = null;

            base.OnDetaching();
        }

        #endregion

        #region Event handler 

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            _popup = GetVisualChild<Popup>(AssociatedObject);
            _thumb = GetVisualChild<Thumb>(AssociatedObject);
            if (_thumb == null || _popup == null) return;
            
            _thumb.DragStarted += Thumb_DragStarted;
            _thumb.DragDelta += Thumb_DragDelta;
            _thumb.DragCompleted += Thumb_DragCompleted;

            AssociatedObject.PreviewKeyDown += Slider_PreviewKeyDown;
            AssociatedObject.KeyUp += Slider_KeyUp;
        }

        #endregion

        #region Event handler < Mouse

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            _popup.HorizontalOffset += +e.HorizontalChange;
            _popup.HorizontalOffset += -e.HorizontalChange;
            _changedHorizontalOffset = e.HorizontalChange;
        }

        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _popup.VerticalOffset = VerticalOffset + _thumb.ActualHeight;
            _popup.IsOpen = true;
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            _popup.IsOpen = false;
        }

        #endregion

        #region Event handler < Keyboard

        private async void Slider_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            await Task.Delay(100);

            if (e.Key == System.Windows.Input.Key.Left ||
                e.Key == System.Windows.Input.Key.Right)
            {
                _popup.VerticalOffset = VerticalOffset + _thumb.ActualHeight;
                _popup.HorizontalOffset += +_changedHorizontalOffset;
                _popup.HorizontalOffset += -_changedHorizontalOffset;
                _popup.IsOpen = true;
            }
        }

        private async void Slider_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            await Task.Delay(100);

            _popup.IsOpen = false;
        }

        #endregion

        #region UI Helper < Common method

        public static T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            if (parent == null) return null;

            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }

            return child;
        }
        
        #endregion
    }
}
