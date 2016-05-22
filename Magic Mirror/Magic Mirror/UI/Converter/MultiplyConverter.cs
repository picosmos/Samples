using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Koopakiller.Apps.MagicMirror.UI.Converter
{
    public class MultiplyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dblValue = value as IConvertible;
            var dblParameter = parameter as IConvertible;
            if (dblValue != null && dblParameter != null)
            {
                return System.Convert.ToDouble(dblValue) * System.Convert.ToDouble(dblParameter);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var dblValue = value as IConvertible;
            var dblParameter = parameter as IConvertible;
            if (dblValue != null && dblParameter != null)
            {
                return System.Convert.ToDouble(dblValue) / System.Convert.ToDouble(dblParameter);
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
