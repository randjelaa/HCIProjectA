using MvvmHelpers;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;

namespace VetClinic.ViewModels
{
    public class AppointmentDetailsViewModel : BaseViewModel
    {
        public Appointment Appointment { get; }
        public ICommand AddMedicalRecordCommand { get; }

        public ICommand SaveRecordCommand { get; }

        private readonly Window _window;

        private bool _isAddingRecord;
        public bool IsAddingRecord
        {
            get => _isAddingRecord;
            set => SetProperty(ref _isAddingRecord, value); // from MvvmHelpers or your base VM
        }

        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string Medications { get; set; }
        public string Notes { get; set; }


        public AppointmentDetailsViewModel(Appointment appointment, Window window)
        {
            Appointment = appointment;
            _window = window;

            AddMedicalRecordCommand = new RelayCommand(AddRecord);
            SaveRecordCommand = new RelayCommand(SaveRecord);
        }

        private void AddRecord(object obj)
        {
            IsAddingRecord = true;
        }

        private void SaveRecord(object obj)
        {
            using var db = new VetClinicContext();

            var record = new Medicalrecord
            {
                AppointmentId = Appointment.Id,
                Diagnosis = Diagnosis,
                Treatment = Treatment,
                Medications = Medications,
                Notes = Notes
            };

            db.Medicalrecords.Add(record);
            db.SaveChanges();

            IsAddingRecord = false; // hide form after saving
        }
    }
}
