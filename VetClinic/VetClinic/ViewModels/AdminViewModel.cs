using Microsoft.VisualBasic;
using MvvmHelpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views;
using VetClinic.Views.Pages;
using VetClinic.Resources;

namespace VetClinic.ViewModels
{
    public class AdminViewModel : BaseViewModel
    {
        public User LoggedInUser { get; }

        public ICommand NavigateUsersCommand { get; }
        public ICommand NavigateServicesCommand { get; }
        public ICommand NavigateReportsCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public ICommand LogoutCommand { get; }

        public Frame FrameRef { get; set; }

        private string _currentPage;
        public string CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public AdminViewModel(User user)
        {
            LoggedInUser = user;

            NavigateUsersCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Users";
                Navigate(new UserManagementPage());
            });


            NavigateServicesCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Services";
                Navigate(new ServiceManagementPage());
            });

            NavigateReportsCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Reports";
                Navigate(new ReportsPage());
            });

            NavigateSettingsCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Settings";
                Navigate(new SettingsPage(LoggedInUser));
            });

            LogoutCommand = new RelayCommand(ExecuteLogout);
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

        private void ExecuteLogout(object obj)
        {
            var result = MessageBox.Show(
                StringResources.ConfirmLogout, // iz Strings.resx
                StringResources.Logout,   // iz Strings.resx
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result != MessageBoxResult.Yes)
                return;

            // Perform any necessary cleanup
            var old = App.Current.MainWindow;

            // Show the LoginView again
            var main = new MainView();
            Application.Current.MainWindow = main;
            main.Show();
            old.Close();
        }
    }
}
