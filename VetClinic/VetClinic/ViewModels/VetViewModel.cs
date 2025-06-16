using MvvmHelpers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views;
using VetClinic.Views.Pages;

namespace VetClinic.ViewModels
{
    public class VetViewModel : BaseViewModel
    {
        public User LoggedInUser { get; }

        public ICommand NavigateActiveAppointmentsCommand { get; }
        public ICommand NavigateClosedAppointmentsCommand { get; }
        public ICommand NavigateReportsCommand { get; }
        public ICommand NavigateSettingsCommand { get; }

        public ICommand LogoutCommand { get; }

        public Frame FrameRef { get; set; }

        public VetViewModel(User user)
        {
            LoggedInUser = user;

            NavigateActiveAppointmentsCommand = new RelayCommand(_ => Navigate(new ActiveAppointmentsPage(LoggedInUser)));
            NavigateClosedAppointmentsCommand = new RelayCommand(_ => Navigate(new ClosedAppointmentsPage(LoggedInUser)));
            NavigateReportsCommand = new RelayCommand(_ => Navigate(new ReportsPage()));
            NavigateSettingsCommand = new RelayCommand(_ => Navigate(new SettingsPage(LoggedInUser)));

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
