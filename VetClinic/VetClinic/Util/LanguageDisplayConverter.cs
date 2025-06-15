using System;
using System.Globalization;
using System.Windows.Data;

namespace VetClinic
{
    public class LanguageDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "en" => "English",
                "sr" => "Srpski",
                _ => "Unknown"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() switch
            {
                "English" => "en",
                "Srpski" => "sr",
                _ => "en"
            };
        }
    }
}
