using System;
using System.Reactive.Linq;
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

            Observable.FromEvent<EventHandler, EventArgs>(
                h => (s, e) => h(e),
                h => AssociatedObject.Closed += h,
                h => AssociatedObject.Closed -= h)
                .Take(1)
                .Subscribe(_ => ((IDisposable)AssociatedObject.DataContext).Dispose());
        }
    }
}