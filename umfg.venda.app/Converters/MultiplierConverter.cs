using System;
using System.Globalization;
using System.Windows.Data;

namespace umfg.venda.app.Converters
{
    internal class MultiplierConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0d;

            if (!double.TryParse(value.ToString(), out var input)) return 0d;

            if (parameter == null) return input;

            if (!double.TryParse(parameter.ToString(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out var factor))
                return input;

            return input * factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
