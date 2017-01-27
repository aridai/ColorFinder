using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ColorFinder.Models
{
    /// <summary>
    /// Win32関数を提供します。
    /// </summary>
    public static class NativeFunctions
    {
        /// <summary>
        /// キーボードの状態を取得します。
        /// </summary>
        /// <param name="nVirtKey">仮想キーコード</param>
        /// <returns>仮想キーの状態</returns>
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        /// <summary>
        /// マウスの座標を取得します。
        /// </summary>
        /// <param name="lpPoint">座標を格納する構造体</param>
        /// <returns>関数が成功した場合はtrue、失敗した場合はfalseを返します。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Point lpPoint);
    }
}