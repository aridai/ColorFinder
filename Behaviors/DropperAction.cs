using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using ColorFinder.Views;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace ColorFinder.Behaviors
{
    /// <summary>
    /// スポイト機能を提供します。
    /// </summary>
    public class DropperAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// スポイトウィンドウを表示します。
        /// </summary>
        /// <param name="parameter"></param>
        protected override void Invoke(object parameter)
        {
            Application.Current.MainWindow.Hide();
            new DropperWindow().ShowDialog();
            Application.Current.MainWindow.Show();
        }
    }
}