using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Sample.Behavior
{
    public class ToolTipToPopupSliderBehavior : Behavior<Slider>
    {
        #region Variable

        private Thumb _thumb;
        private Popup _popup;

        #endregion

        #region Proteced method 

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject == null) return;
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.PreviewKeyUp -= Slider_PreviewKeyUp;

            if (_thumb == null) return;
            //_thumb.QueryCursor -= _thumb_QueryCursor;
            //_thumb.MouseMove -= _thumb_MouseMove;
            _thumb.MouseLeave -= _thumb_MouseLeave;

            _thumb = null;
            _popup = null;

            base.OnDetaching();
        }

        #endregion

        #region Event handler 

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            _thumb = GetVisualChild<Thumb>(AssociatedObject);
            _popup = GetVisualChild<Popup>(AssociatedObject);
            //_thumb = GetVisualChildByName<Thumb>(AssociatedObject, "Thumb");
            //_popup = GetVisualChildByName<Popup>(AssociatedObject, "PART_ToolTipPopup");

            if (_thumb == null) return;
            //_thumb.QueryCursor += _thumb_QueryCursor;
            //_thumb.MouseMove += _thumb_MouseMove;
            _thumb.MouseLeave += _thumb_MouseLeave;

            AssociatedObject.PreviewKeyUp += Slider_PreviewKeyUp;
            AssociatedObject.ValueChanged += AssociatedObject_ValueChanged;
        }

        private void AssociatedObject_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _popup.HorizontalOffset = 0;
            _popup.IsOpen = true;

            var xMoveOffset = AssociatedObject.ActualWidth / (AssociatedObject.Maximum - AssociatedObject.Minimum);
            if (e.NewValue > e.OldValue && e.NewValue != AssociatedObject.Maximum)
            {
                _popup.HorizontalOffset += xMoveOffset;
            }
            else if (e.NewValue < e.OldValue && e.OldValue != AssociatedObject.Maximum)
            {
                _popup.HorizontalOffset -= xMoveOffset;
            }
        }

        #region Event handler < Mouse

        //private void _thumb_QueryCursor(object sender, QueryCursorEventArgs e)
        //{
        //    _popup.IsOpen = true;        
        //}

        //private void _thumb_MouseMove(object sender, MouseEventArgs e)
        //{
        //    _popup.IsOpen = true;
        //}

        private void _thumb_MouseLeave(object sender, MouseEventArgs e)
        {
            _popup.IsOpen = false;
        }

        #endregion

        #region Event handler < Keyboard
                
        private void Slider_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            _popup.IsOpen = false;
        }

        #endregion

        #endregion

        #region UI Helper < Common 

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

        public static T GetVisualChildByName<T>(DependencyObject parent, string childName) where T : Visual
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
                    child = GetVisualChildByName<T>(v, childName);
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
