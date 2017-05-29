using System;
using Microsoft.Practices.Prism.Mvvm;

namespace ColorFinder.Models
{
    /// <summary>
    /// マウスカーソルを管理します。
    /// </summary>
    public class Cursor : BindableBase
    {
        private int x;
        private int y;
        private bool isClicked;

        /// <summary>
        /// X座標を取得します。
        /// </summary>
        public int X { get => x; set => SetProperty(ref x, value); }

        /// <summary>
        /// Y座標を取得します。
        /// </summary>
        public int Y { get => y; set => SetProperty(ref y, value); }

        /// <summary>
        /// クリックされているかどうかを取得します。
        /// </summary>
        public bool IsClicked { get => isClicked; set => SetProperty(ref isClicked, value); }
    }
}