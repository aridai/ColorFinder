using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using static ColorFinder.Models.NativeFunctions;

namespace ColorFinder.Models
{
    /// <summary>
    /// マウスカーソルを管理します。
    /// </summary>
    public class MouseCursor : BindableBase
    {
        private int _X, _Y;
        private bool _IsClicked;

        /// <summary>
        /// X座標を取得します。
        /// </summary>
        public int X { get { return _X; } set { SetProperty(ref _X, value); } }

        /// <summary>
        /// Y座標を取得します。
        /// </summary>
        public int Y { get { return _Y; } set { SetProperty(ref _Y, value); } }

        /// <summary>
        /// クリックされているかどうかを取得します。
        /// </summary>
        public bool IsClicked { get { return _IsClicked; } private set { SetProperty(ref _IsClicked, value); } }

        /// <summary>
        /// マウスの状態を更新します。
        /// </summary>
        public void Update()
        {
            //  座標を取得する
            Point point;
            GetCursorPos(out point);
            X = point.X;
            Y = point.Y;

            //  左クリックを判定する
            IsClicked = GetKeyState(0x01) < 0;
        }
    }
}