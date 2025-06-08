using System.Windows.Input;
using System.Windows.Controls;
using VetClinic.Models;
using VetClinic.Views.Pages;
using MvvmHelpers;

namespace VetClinic.ViewModels
{
    public class AdminMainWindowViewModel : BaseViewModel
    {
        public User LoggedInUser { get; }

        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateServicesCommand { get; }
        public ICommand NavigateReportsCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public Frame FrameRef { get; set; }

        public AdminMainWindowViewModel(User user)
        {
            LoggedInUser = user;

            NavigateUsersCommand = new RelayCommand(_ => Navigate(new UserManagementPage()));
            NavigateServicesCommand = new RelayCommand(_ => Navigate(null)); // Replace with real page
            NavigateReportsCommand = new RelayCommand(_ => Navigate(null));  // Replace with real page
            NavigateSettingsCommand = new RelayCommand(_ => Navigate(null)); // Replace with real page
        }

        private void Navigate(Page page)
        {
            if (page == null)
            {
                System.Windows.MessageBox.Show("Page not implemented yet.");
                return;
            }

            FrameRef?.Navigate(page);
        }
    }
}
