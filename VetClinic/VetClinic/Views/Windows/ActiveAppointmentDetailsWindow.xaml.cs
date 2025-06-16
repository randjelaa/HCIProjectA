using System.Windows;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Windows
{
    public partial class ActiveAppointmentDetailsWindow : Window
    {
        public ActiveAppointmentDetailsWindow(Appointment appointment)
        {
            InitializeComponent();
            DataContext = new ActiveAppointmentDetailsViewModel(appointment, this);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
