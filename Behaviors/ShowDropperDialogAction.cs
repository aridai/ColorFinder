﻿using System;
using System.Windows;
using System.Windows.Interactivity;
using ColorFinder.Models;
using ColorFinder.ViewModels;
using ColorFinder.Views;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace ColorFinder.Behaviors
{
    /// <summary>
    /// スポイト機能を提供します。
    /// </summary>
    public class ShowDropperDialogAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var arg = parameter as InteractionRequestedEventArgs;
            var confirmation = arg.Context as Confirmation;
            var mainWindow = AssociatedObject as MainWindow;
            var dropperWindow = new DropperWindow();
            var dropperWindowViewModel = dropperWindow.DataContext as DropperWindowViewModel;

            mainWindow.Hide();
            dropperWindow.ShowDialog();
            mainWindow.Show();
            mainWindow.Activate();

            confirmation.Confirmed = dropperWindowViewModel.Confirmed;
            confirmation.Content = new ColorCode { R = dropperWindowViewModel.R.Value, G = dropperWindowViewModel.G.Value, B = dropperWindowViewModel.B.Value };
            arg.Callback();
        }
    }
}