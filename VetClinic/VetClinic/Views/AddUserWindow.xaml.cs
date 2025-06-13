using System.Windows;
using VetClinic.Models;

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
            DialogResult = true;
        }
    }
}
