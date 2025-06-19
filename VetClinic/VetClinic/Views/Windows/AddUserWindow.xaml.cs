using System.Windows;
using VetClinic.Models;
using VetClinic.Resources;

namespace VetClinic.Views.Windows
{
    public partial class AddUserWindow : Window
    {
        public AddUserViewModel ViewModel => (AddUserViewModel)DataContext;

        public string Password => PasswordBox.Password;

        public AddUserWindow()
        {
            InitializeComponent();
            DataContext = new AddUserViewModel();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                StringResources.SaveSuccessMessage,
                StringResources.SuccessTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
