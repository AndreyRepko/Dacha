using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFUtils.Converters
{
    public class BoolToColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Colors.Black;

            if (value.GetType() != typeof (bool))
                throw new ArgumentException(nameof(value),$"{value.GetType()} found but simple bool expected");
            var boolValue = (bool) value;

            if (boolValue)
                return Colors.Green;
            else
            {
                return Colors.Coral;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
