using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VetClinic.Util
{
    public static class ThemeManager
    {
        public static void ApplyTheme(string theme)
        {
            /*ResourceDictionary newTheme = new();
            switch (theme.ToLower())
            {
                case "dark":
                    newTheme.Source = new Uri("Themes/Dark.xaml", UriKind.Relative);
                    break;
                case "blue":
                    newTheme.Source = new Uri("Themes/Blue.xaml", UriKind.Relative);
                    break;
                default:
                    newTheme.Source = new Uri("Themes/Light.xaml", UriKind.Relative);
                    break;
            }

            var existing = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source?.OriginalString.StartsWith("Themes/") == true);

            if (existing != null)
                Application.Current.Resources.MergedDictionaries.Remove(existing);

            Application.Current.Resources.MergedDictionaries.Add(newTheme);
            */
        }
    }
}
