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
            string oldPass = OldPasswordBox.Password;
            string newPass = NewPasswordBox.Password;
            string selectedCulture = ((SettingsViewModel)DataContext).SelectedLanguage;

            ((SettingsViewModel)DataContext).SaveChanges(
                selectedCultureCode: selectedCulture,
                oldPasswordInput: PasswordChangePanel.Visibility == Visibility.Visible ? oldPass : null,
                newPasswordInput: PasswordChangePanel.Visibility == Visibility.Visible ? newPass : null
            );
        }


        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordChangePanel.Visibility =
                PasswordChangePanel.Visibility == Visibility.Visible
                    ? Visibility.Collapsed
                    : Visibility.Visible;
        }

    }
}
