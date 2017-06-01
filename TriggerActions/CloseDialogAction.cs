using System;
using System.Windows;
using System.Windows.Interactivity;

namespace ColorFinder.TriggerActions
{
    /// <summary>
    /// ダイアログを閉じるアクションを提供します。
    /// </summary>
    public class CloseDialogAction : TriggerAction<Window>
    {
        protected override void Invoke(object parameter)
        {
            AssociatedObject.Close();
        }
    }
}