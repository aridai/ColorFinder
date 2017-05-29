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

        /// <summary>
        /// 座標を指定してインスタンスを生成します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}