using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views;
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

        private User LoggedInUser;

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
            set
            {
                if (selectedTheme != value)
                {
                    selectedTheme = value;
                    //ApplyTheme(selectedTheme);
                    //OnPropertyChanged();
                }
            }
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
            }

            SaveCommand = new RelayCommand(_ => SaveChanges());
        }

        private void SaveChanges()
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

                //pref.Theme = SelectedTheme;
                db.SaveChanges();

                MessageBox.Show("Changes saved.", "Settings", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void ChangeLanguage(string cultureCode)
        {
            App.ChangeCulture(cultureCode);

            // Save to DB
            using var db = new VetClinicContext();
            var pref = db.Userpreferences.FirstOrDefault(p => p.UserId == LoggedInUser.Id);
            if (pref == null)
            {
                pref = new Userpreference { UserId = LoggedInUser.Id };
                db.Userpreferences.Add(pref);
            }

            pref.Language = cultureCode;
            db.SaveChanges();

            // Apply language immediately by recreating the main window
            var mainWindow = App.Current.MainWindow;
            Window newMain;

            if (LoggedInUser.RoleId == 2)
            {
                newMain = new AdminView(LoggedInUser);
            }
            else
            {
                newMain = new VetView(LoggedInUser);
            }

            App.Current.MainWindow = newMain;
            newMain.Show();
            mainWindow.Close();
        }

        private void ApplyTheme(string theme)
        {
            ThemeManager.ApplyTheme(theme);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
