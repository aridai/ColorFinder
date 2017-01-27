using System;
using System.Runtime.InteropServices;

namespace ColorFinder.Models
{
    /// <summary>
    /// 地点の座標を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        /// <summary>
        /// X座標
        /// </summary>
        public int X;

        /// <summary>
        /// Y座標
        /// </summary>
        public int Y;
    }
}