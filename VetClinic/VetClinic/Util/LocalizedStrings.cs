using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic
{
    public class LocalizedStrings : INotifyPropertyChanged
    {
        public static ResourceManager ResourceManager => Resources.StringResources.ResourceManager;

        public string this[string key] => ResourceManager.GetString(key, CultureInfo.CurrentUICulture);

        public event PropertyChangedEventHandler PropertyChanged;

        public void Refresh()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); // Refresh all bindings
        }
    }

}
