using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StackBarTest2
{
    /// <summary>
    /// Parent Value, Child Value, Parent Width
    /// </summary>
    public class ProportionalValueWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var parentValue = (double) values[0];
            var childValue = (double)values[1];
            var parentWidth = (double)values[2];
            return (parentWidth * childValue) / parentValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
