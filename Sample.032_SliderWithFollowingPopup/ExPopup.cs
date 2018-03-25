using System;
using System.Windows.Controls.Primitives;

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
    }
}
