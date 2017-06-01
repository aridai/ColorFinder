using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ColorFinder.Converters
{
    /// <summary>
    /// カラーコードをSolidColorBrushに変換するコンバータを提供します。
    /// </summary>
    public class SolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Models.ColorCode color)
            {
                return new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
            }

            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("SolidColorBrushからColorCodeへの変換はサポートされていません。");
        }
    }
}