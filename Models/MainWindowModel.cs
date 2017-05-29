using System;

namespace ColorFinder.Models
{
    /// <summary>
    /// MainWindowで必要なモデルを提供します。
    /// </summary>
    public class MainWindowModel
    {
        /// <summary>
        /// カラーコードを取得します。
        /// </summary>
        public ColorCode ColorCode { get; } = new ColorCode();
    }
}