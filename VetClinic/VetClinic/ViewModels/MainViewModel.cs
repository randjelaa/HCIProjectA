// ViewModels/MainWindowViewModel.cs
using MvvmHelpers;
using System.Windows;
using System.Windows.Input;
using VetClinic.Views;

namespace VetClinic.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }

        public MainViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin(object obj)
        {
            var loginView = new LoginView();
            var loginViewModel = (LoginViewModel)loginView.DataContext;

            var result = loginView.ShowDialog();

            if (result == true && loginViewModel.LoggedInUser != null)
            {
                var role = loginViewModel.LoggedInUser.Role?.Name?.ToLower();

                Window roleWindow = role switch
                {
                    "admin" or "administrator" => new AdminView(loginViewModel.LoggedInUser),
                    "vet" or "veterinarian" => new VetView(loginViewModel.LoggedInUser),
                    _ => throw new InvalidOperationException("Unknown user role")
                };

                if (roleWindow != null)
                {
                    var old = Application.Current.MainWindow;
                    Application.Current.MainWindow = roleWindow;
                    roleWindow.Show();
                    old.Close(); // close the initial main window
                }
            }
        }
    }
}
