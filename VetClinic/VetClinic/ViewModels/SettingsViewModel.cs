using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views;
using VetClinic.Views.Pages;
using VetClinic.Util;

namespace VetClinic.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private string email;
        private string password;
        private string selectedTheme;
        private string selectedLanguage;

        private readonly User LoggedInUser;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public string SelectedTheme
        {
            get => selectedTheme;
            set { selectedTheme = value; OnPropertyChanged(); }
        }

        public string SelectedLanguage
        {
            get => selectedLanguage;
            set { selectedLanguage = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public SettingsViewModel(User loggedInUser)
        {
            LoggedInUser = loggedInUser;

            if (LoggedInUser != null)
            {
                Name = LoggedInUser.Name;
                Email = LoggedInUser.Email;

                using var db = new VetClinicContext();
                var pref = db.Userpreferences.FirstOrDefault(p => p.UserId == LoggedInUser.Id);
                SelectedTheme = pref?.Theme ?? "Light";
                SelectedLanguage = pref?.Language ?? "en";
            }

            SaveCommand = new RelayCommand(_ => SaveChanges(SelectedLanguage));
        }

        public void SaveChanges(string selectedCultureCode)
        {
            using var db = new VetClinicContext();
            var user = db.Users.FirstOrDefault(u => u.Id == LoggedInUser.Id);

            if (user != null)
            {
                user.Name = Name;
                user.Email = Email;
                if (!string.IsNullOrWhiteSpace(Password))
                    user.Password = Password;

                var pref = db.Userpreferences.FirstOrDefault(p => p.UserId == user.Id);
                if (pref == null)
                {
                    pref = new Userpreference { UserId = user.Id };
                    db.Userpreferences.Add(pref);
                }

                if (!string.IsNullOrWhiteSpace(selectedCultureCode))
                    pref.Language = selectedCultureCode;

                db.SaveChanges();
            }

            if (!string.IsNullOrWhiteSpace(selectedCultureCode))
                App.ChangeCulture(selectedCultureCode);

            // Reopen main window and return to Settings
            var oldWindow = Application.Current.MainWindow;
            Window newWindow;

            if (LoggedInUser.RoleId == 2)
            {
                newWindow = new AdminView(LoggedInUser);
                Application.Current.MainWindow = newWindow;
                newWindow.Show();
                oldWindow.Close();
                ((AdminView)newWindow).MainFrame.Navigate(new SettingsPage(LoggedInUser));
            }
            else
            {
                newWindow = new VetView(LoggedInUser);
                Application.Current.MainWindow = newWindow;
                newWindow.Show();
                oldWindow.Close();
                ((VetView)newWindow).MainFrame.Navigate(new SettingsPage(LoggedInUser));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
