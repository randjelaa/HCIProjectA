using System.Windows;
using System.Windows.Controls;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Pages
{
    public partial class SettingsPage : Page
    {
        public User LoggedInUser { get; }

        public SettingsPage(User loggedInUser)
        {
            InitializeComponent();
            LoggedInUser = loggedInUser;
            DataContext = new SettingsViewModel(loggedInUser);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }

        private string _selectedCultureCode;

        private void LanguageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string cultureCode)
            {
                _selectedCultureCode = cultureCode; // just store for later
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
            {
                vm.SaveChanges(_selectedCultureCode);
            }
        }
    }
}
