using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;

namespace ColorFinder.Models
{
    /// <summary>
    /// カラーコードのRGB値を管理します。
    /// </summary>
    public class ColorCode : BindableBase
    {
        private byte _R, _G, _B;

        /// <summary>
        /// R値を取得・設定します。
        /// </summary>
        public byte R
        {
            get { return _R; }
            set { SetProperty(ref _R, value); }
        }

        /// <summary>
        /// G値を取得・設定します。
        /// </summary>
        public byte G
        {
            get { return _G; }
            set { SetProperty(ref _G, value); }
        }

        /// <summary>
        /// B値を取得・設定します。
        /// </summary>
        public byte B
        {
            get { return _B; }
            set { SetProperty(ref _B, value); }
        }

        /// <summary>
        /// ランダムにRGB値を設定します。
        /// </summary>
        public void SetRandomly()
        {
            var random = new Random();
            R = (byte)random.Next(255);
            G = (byte)random.Next(255);
            B = (byte)random.Next(255);
        }
    }
}