using System.Windows;
using System.Windows.Controls;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Pages
{
    public partial class ClosedAppointmentsPage : Page
    {
        public ClosedAppointmentsPage(User vet)
        {
            InitializeComponent();
            DataContext = new ClosedAppointmentsViewModel(vet);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ((ClosedAppointmentsViewModel)DataContext).SelectedDate = null;
        }
    }
}
