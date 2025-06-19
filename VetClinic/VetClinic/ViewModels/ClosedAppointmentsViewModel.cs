using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views.Windows;

namespace VetClinic.ViewModels
{
    public class ClosedAppointmentsViewModel : BaseViewModel
    {
        private readonly User loggedInVet;

        public ObservableCollection<Appointment> FilteredAppointments { get; set; } = new();

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

        private List<Appointment> allAppointments = new();

        public bool HasAppointments => FilteredAppointments.Any();

        public ClosedAppointmentsViewModel(User vet)
        {
            loggedInVet = vet;
            ViewAppointmentCommand = new RelayCommand(ViewAppointment);
            LoadAppointments();
        }

        private void LoadAppointments()
        {
            using var db = new VetClinicContext();

            var appointments = db.Appointments
                .Where(a => a.VetId == loggedInVet.Id && a.Deleted == null)
                .OrderBy(a => a.Date)
                .ToList();

            foreach (var a in appointments)
            {
                db.Entry(a).Reference(x => x.Pet).Load();
                db.Entry(a).Reference(x => x.Service).Load();
                db.Entry(a.Pet).Reference(x => x.Owner).Load();
                db.Entry(a).Collection(x => x.Medicalrecords).Load(); // ← THIS LINE IS MISSING
            }

            // Filter to only those that have medical records
            allAppointments = appointments.Where(a => a.Medicalrecords.Any()).ToList();
            FilterAppointments();
        }

        private void FilterAppointments()
        {
            var filtered = allAppointments
                .Where(a => SelectedDate == null || a.Date.Date == SelectedDate.Value.Date)
                .ToList();

            FilteredAppointments.Clear();
            filtered.ForEach(FilteredAppointments.Add);
            OnPropertyChanged(nameof(HasAppointments));
        }

        private void ViewAppointment(object parameter)
        {
            if (parameter is Appointment appointment)
            {
                var detailsWindow = new ClosedAppointmentDetailsWindow(appointment);
                detailsWindow.ShowDialog();
            }
        }
    }
}
