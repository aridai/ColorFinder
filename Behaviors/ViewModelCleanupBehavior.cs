using System;
using System.Windows;
using System.Windows.Interactivity;

namespace ColorFinder.Behaviors
{
    /// <summary>
    /// ViewModelのリソースの解放を行うビヘイビアを提供します。
    /// </summary>
    public class ViewModelCleanupBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed += windowClosed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= windowClosed;
        }

        private void windowClosed(object sender, EventArgs e)
        {
            (AssociatedObject.DataContext as IDisposable)?.Dispose();
        }
    }
}