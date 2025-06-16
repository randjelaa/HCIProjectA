using System.Windows;
using System.Windows.Controls;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Pages
{
    public partial class ActiveAppointmentsPage : Page
    {
        public ActiveAppointmentsPage(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new ActiveAppointmentsViewModel(loggedInUser);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ((ActiveAppointmentsViewModel)DataContext).SelectedDate = null;
        }
    }
}
