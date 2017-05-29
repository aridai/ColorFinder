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
        private bool hasOnDetachingCalled;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed += WindowClosed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= WindowClosed;
            hasOnDetachingCalled = true;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            (AssociatedObject.DataContext as IDisposable)?.Dispose();
        }

        ~ViewModelCleanupBehavior()
        {
            if (!hasOnDetachingCalled)
                OnDetaching();
        }
    }
}