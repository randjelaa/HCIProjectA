using System.Windows;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Windows
{
    public partial class AppointmentDetailsWindow : Window
    {
        public AppointmentDetailsWindow(Appointment appointment)
        {
            InitializeComponent();
            DataContext = new AppointmentDetailsViewModel(appointment, this);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
