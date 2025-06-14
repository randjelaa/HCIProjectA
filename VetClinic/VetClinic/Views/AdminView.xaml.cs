using System.Windows;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views
{
    public partial class AdminView : Window
    {
        public AdminView(User loggedInUser)
        {
            InitializeComponent();
            var vm = new AdminViewModel(loggedInUser);
            DataContext = vm;
            vm.FrameRef = MainFrame;

            MainFrame.Navigate(new Pages.UserManagementPage());
        }
    }
}
