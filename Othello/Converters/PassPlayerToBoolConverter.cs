using System;
using System.Globalization;
using System.Windows.Data;

namespace Othello.Converters
{
    public class PassPlayerToBoolConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            if (_value is string value)
            {
                if (value.Equals(""))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            return false;
        }
    }
}
