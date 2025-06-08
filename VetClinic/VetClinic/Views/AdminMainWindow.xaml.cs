using System.Windows;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views
{
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow(User loggedInUser)
        {
            InitializeComponent();
            var vm = new AdminMainWindowViewModel(loggedInUser);
            DataContext = vm;
            vm.FrameRef = MainFrame;

            MainFrame.Navigate(new Pages.UserManagementPage());
        }
    }
}
