using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Sample.Behavior
{
    public class KeyEventOpenSliderToolTipBehavior : Behavior<Slider>
    {
        private Thumb _thumb;
        private Popup _popup;
        private double _changedHorizontalOffset;
        
        public int VerticalOffset
        {
            get { return (int)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register(
                "VerticalOffset", 
                typeof(int), 
                typeof(KeyEventOpenSliderToolTipBehavior), 
                new PropertyMetadata(0));

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            _thumb.DragDelta -= ValueThumb_DragDelta;
            _thumb.DragStarted -= ValueThumb_DragStarted;
            _thumb.DragCompleted -= ValueThumb_DragCompleted;

            AssociatedObject.PreviewKeyDown -= Slider_PreviewKeyDown;
            AssociatedObject.KeyUp -= Slider_KeyUp;

            _thumb = null;
            _popup = null;

            base.OnDetaching();
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            //var _slider = AssociatedObject as Slider;
            //if (_slider == null) return;
            _popup = GetVisualChild<Popup>(AssociatedObject);
            _thumb = GetVisualChild<Thumb>(AssociatedObject);
            if (_thumb == null || _popup == null) return;
            
            _thumb.DragStarted += ValueThumb_DragStarted;
            _thumb.DragDelta += ValueThumb_DragDelta;
            _thumb.DragCompleted += ValueThumb_DragCompleted;

            AssociatedObject.PreviewKeyDown += Slider_PreviewKeyDown;
            AssociatedObject.KeyUp += Slider_KeyUp;
        }

        private void ValueThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            _popup.HorizontalOffset += +e.HorizontalChange;
            _popup.HorizontalOffset += -e.HorizontalChange;
            _changedHorizontalOffset = e.HorizontalChange;
        }

        private void ValueThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            _popup.VerticalOffset = VerticalOffset + _thumb.ActualHeight;
            _popup.IsOpen = true;
        }

        private void ValueThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            _popup.IsOpen = false;
        }
        
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

        // UIHelper
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
    }
}
