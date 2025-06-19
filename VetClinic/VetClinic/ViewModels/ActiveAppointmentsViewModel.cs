using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views.Windows;

namespace VetClinic.ViewModels
{
    public class ActiveAppointmentsViewModel : BaseViewModel
    {
        private readonly User loggedInVet;
        public ObservableCollection<Appointment> UpcomingAppointments { get; set; } = new();
        public ObservableCollection<Appointment> MissedAppointments { get; set; } = new();

        public DateTime? SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged();
                FilterAppointments();
            }
        }
        private DateTime? selectedDate;

        public Appointment SelectedAppointment { get; set; }

        public ICommand ViewAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }
        public ICommand ClearFilterCommand { get; }

        public ActiveAppointmentsViewModel(User vet)
        {
            loggedInVet = vet;
            ViewAppointmentCommand = new RelayCommand(ViewAppointment);
            DeleteAppointmentCommand = new RelayCommand(DeleteAppointment);
            ClearFilterCommand = new RelayCommand(_ =>
            {
                SelectedDate = null;
                FilterAppointments();
            });

            LoadAppointments();
        }

        private List<Appointment> allAppointments = new();

        private void LoadAppointments()
        {
            using var db = new VetClinicContext();

            var appointments = db.Appointments
                .Where(a => a.VetId == loggedInVet.Id && a.Deleted == null)
                .OrderBy(a => a.Date)
                .ToList();

            // Eager load Pet and Service
            foreach (var appt in appointments)
            {
                db.Entry(appt).Reference(a => a.Pet).Load();
                db.Entry(appt).Reference(a => a.Service).Load();
                db.Entry(appt).Collection(a => a.Medicalrecords).Load();
            }

            allAppointments = appointments;
            FilterAppointments();
        }

        private void FilterAppointments()
        {
            var today = DateTime.Today;

            var upcoming = allAppointments
                .Where(a => a.Date >= today && (SelectedDate == null || a.Date.Date == SelectedDate.Value.Date))
                .ToList();

            var missed = allAppointments
                .Where(a => a.Date < today && !a.Medicalrecords.Any())
                .ToList();

            UpcomingAppointments.Clear();
            MissedAppointments.Clear();

            upcoming.ForEach(UpcomingAppointments.Add);
            missed.ForEach(MissedAppointments.Add);
        }

        private void ViewAppointment(object parameter)
        {
            if (parameter is Appointment appointment)
            {
                using var db = new VetClinicContext();
                db.Entry(appointment).Reference(a => a.Pet).Load();
                db.Entry(appointment).Reference(a => a.Service).Load();
                db.Entry(appointment.Pet).Reference(p => p.Owner).Load();

                var detailsView = new ActiveAppointmentDetailsWindow(appointment);
                detailsView.ShowDialog();

                // Reload appointments in case a medical record was added
                LoadAppointments();
            }
        }

        private void DeleteAppointment(object parameter)
        {
            if (parameter is not Appointment appointment)
                return;

            var confirmDialog = new ConfirmDeleteWindow
            {
                Owner = Application.Current.MainWindow
            };

            bool? result = confirmDialog.ShowDialog();
            if (result != true || !confirmDialog.Confirmed)
                return;

            using var db = new VetClinicContext();
            var toDelete = db.Appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (toDelete == null) return;

            toDelete.Deleted = DateTime.Now;
            db.SaveChanges();

            LoadAppointments(); // refresh the list after deletion
        }
    }
}
