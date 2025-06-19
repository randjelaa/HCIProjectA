using MvvmHelpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Resources;
using VetClinic.Views;
using VetClinic.Views.Pages;

namespace VetClinic.ViewModels
{
    public class VetViewModel : BaseViewModel
    {
        public User LoggedInUser { get; }

        public ICommand NavigateActiveAppointmentsCommand { get; }
        public ICommand NavigateClosedAppointmentsCommand { get; }
        public ICommand NavigatePetsCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public ICommand LogoutCommand { get; }

        public Frame FrameRef { get; set; }

        private string _currentPage;
        public string CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public VetViewModel(User user)
        {
            LoggedInUser = user;

            NavigateActiveAppointmentsCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Active";
                Navigate(new ActiveAppointmentsPage(LoggedInUser));
            });
            NavigateClosedAppointmentsCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Closed";
                Navigate(new ClosedAppointmentsPage(LoggedInUser));
            });
            NavigatePetsCommand = new RelayCommand(_ =>
            {
                CurrentPage = "Pets";
                Navigate(new PetsPage());
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
                StringResources.ConfirmLogout, 
                StringResources.Logout,   
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
