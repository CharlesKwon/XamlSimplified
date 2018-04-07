using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Sample.Control
{
    public class FollowingPopup : Popup
    {
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            this.PlacementTarget.PreviewMouseMove += PlacementTarget_PreviewMouseMove;
        }

        protected override void OnClosed(EventArgs e)
        {
            this.PlacementTarget.PreviewMouseMove -= PlacementTarget_PreviewMouseMove;

            base.OnClosed(e);
        }

        private void PlacementTarget_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.HorizontalOffset += +0.1;
            this.HorizontalOffset += -0.1;
        }

        #region UI Helper < Common 

        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }

        #endregion
    }
}
