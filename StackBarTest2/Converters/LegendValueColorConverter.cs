using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace StackBarTest2.Converters
{
    public class LegendValueColorConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var legend = (Dictionary<string, Color>)values[0];
            var value = values[1].ToString();
            return legend.ContainsKey(value) ? legend[value] : throw new ArgumentException("Value not found in Legend");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
