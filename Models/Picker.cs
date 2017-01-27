using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorFinder.Models
{
    /// <summary>
    /// 色の抽出処理を提供します。
    /// </summary>
    public class Picker
    {
        /// <summary>
        /// 指定した地点の色を抽出します。
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>指定した地点の色データを返します。</returns>
        public Color PickUpColor(int x, int y)
        {
            var bitmap = new Bitmap(1, 1);
            var graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(new System.Drawing.Point(x, y), new System.Drawing.Point(0, 0), new Size(1, 1));
            return bitmap.GetPixel(0, 0);
        }
    }
}
