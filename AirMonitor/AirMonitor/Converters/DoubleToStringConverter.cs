using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AirMonitor.Converters
{
    /// <summary>
    /// Double to Percentage Converter
    /// </summary>
    class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{(double)value*100}%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Double.Parse((string)value)/100;
        }
    }
}
