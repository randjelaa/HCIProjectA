using System.Windows;
using System.Windows.Controls;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Pages
{
    public partial class AppointmentsPage : Page
    {
        public AppointmentsPage(User loggedInUser)
        {
            InitializeComponent();
            DataContext = new AppointmentsViewModel(loggedInUser);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ((AppointmentsViewModel)DataContext).SelectedDate = null;
        }
    }
}
