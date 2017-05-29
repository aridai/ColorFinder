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
        private bool HasOnDetachingCalled;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Closed += WindowClosed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Closed -= WindowClosed;
            HasOnDetachingCalled = true;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            (AssociatedObject.DataContext as IDisposable)?.Dispose();
        }

        ~ViewModelCleanupBehavior()
        {
            if (!HasOnDetachingCalled)
                OnDetaching();
        }
    }
}