using System.Linq;
using System.Windows;
using VetClinic.Models;

namespace VetClinic.Views.Windows
{
    public partial class ClosedAppointmentDetailsWindow : Window
    {
        public Appointment Appointment { get; }
        public Medicalrecord MedicalRecord { get; }

        public ClosedAppointmentDetailsWindow(Appointment appointment)
        {
            InitializeComponent();
            Appointment = appointment;
            MedicalRecord = appointment.Medicalrecords.FirstOrDefault(); // assuming only one per appointment
            DataContext = this;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
