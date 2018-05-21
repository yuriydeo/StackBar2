using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Converters;

namespace StackBarTest2.Converters
{
    public class ProportionalValueWidthWithHeaderConverter : IMultiValueConverter
    {
        /// <summary>
        /// Parent Value, Child Value, Parent Width, Header Width
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var parentValue = (double)values[0];
            var childValue = (double)values[1];
            var parentWidth = (double)values[2];
            var headerWidth = (double) values[3];
            return ((parentWidth - headerWidth) * childValue) / parentValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
