using System;
using System.Windows;
using System.Windows.Interactivity;
using ColorFinder.Models;
using ColorFinder.ViewModels;
using ColorFinder.Views;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace ColorFinder.Behaviors
{
    /// <summary>
    /// スポイトウィンドウを表示します。
    /// </summary>
    public class ShowDropperDialogAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            var arg = (InteractionRequestedEventArgs)parameter;
            var confirmation = (Confirmation)arg.Context;
            var dropperWindow = new DropperWindow();
            var dropperWindowViewModel = (DropperWindowViewModel)dropperWindow.DataContext;

            AssociatedObject.Hide();
            dropperWindow.ShowDialog();
            AssociatedObject.Show();
            AssociatedObject.Activate();

            confirmation.Confirmed = dropperWindowViewModel.Confirmed;
            confirmation.Content = new ColorCode { R = dropperWindowViewModel.R.Value, G = dropperWindowViewModel.G.Value, B = dropperWindowViewModel.B.Value };
            arg.Callback();
        }
    }
}