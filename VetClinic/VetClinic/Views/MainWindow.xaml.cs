using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VetClinic.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginView = new LoginView();
            var loginViewModel = (LoginViewModel)loginView.DataContext;

            var result = loginView.ShowDialog();

            if (result == true && loginViewModel.LoggedInUser != null)
            {
                var role = loginViewModel.LoggedInUser.Role?.Name?.ToLower();

                Window roleWindow = role switch
                {
                    "admin" or "administrator" => new AdminMainWindow(loginViewModel.LoggedInUser),
                    "vet" or "veterinarian" => new VetMainWindow(loginViewModel.LoggedInUser),
                    _ => throw new InvalidOperationException("Unknown user role")
                };

                if (roleWindow != null)
                {
                    Application.Current.MainWindow = roleWindow;
                    roleWindow.Show();
                    this.Close(); // close the initial main window
                }
            }
        }

    }
}
