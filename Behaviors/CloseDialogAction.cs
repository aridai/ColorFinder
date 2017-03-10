using System;
using System.Windows;
using System.Windows.Interactivity;
using ColorFinder.Views;

namespace ColorFinder.Behaviors
{
    public class CloseDialogAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var dialog = AssociatedObject as DropperWindow;
            dialog.Close();
        }
    }
}