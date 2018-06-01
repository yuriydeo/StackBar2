using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace StackBarControlLib.Converters
{
    /// <summary>
    /// Param1: Value
    /// Param2: Scale
    /// Converts Value and Scale to element Width
    /// </summary>
    public class ValueScaleToWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var value = (double)values[0];
            var scale = (double)values[1];
            return value * scale;

            //double result = value * scale;
            //return Math.Floor(result*10)/10;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
