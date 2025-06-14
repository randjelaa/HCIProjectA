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

        private void LanguageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string cultureCode)
            {
                App.ChangeCulture(cultureCode);
                var old = Application.Current.MainWindow;

                // You decide which to reopen
                Window newMain;

                if (LoggedInUser.RoleId == 2)
                {
                    newMain = new AdminView(LoggedInUser);
                }
                else
                {
                    newMain = new VetView(LoggedInUser);
                }
                Application.Current.MainWindow = newMain;
                newMain.Show();
                old.Close();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel vm)
                vm.Password = ((PasswordBox)sender).Password;
        }
    }
}
