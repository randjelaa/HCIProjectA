using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;

namespace VetClinic.ViewModels
{
    public class MedicalRecordsViewModel : BaseViewModel
    {
        public ObservableCollection<Medicalrecord> MedicalRecords { get; set; } = new();
        private List<Medicalrecord> allRecords = new();

        public string PetInfo { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public ICommand FilterCommand { get; }

        private readonly Pet pet;

        public MedicalRecordsViewModel(Pet pet)
        {
            this.pet = pet;
            FilterCommand = new RelayCommand(FilterRecords);
            LoadRecords();
        }

        private void LoadRecords()
        {
            using var db = new VetClinicContext();
            var records = db.Medicalrecords
                .Where(r => r.Appointment.PetId == pet.Id)
                .ToList();

            foreach (var record in records)
                db.Entry(record).Reference(r => r.Appointment).Load();

            PetInfo = $"{pet.Name} ({pet.Species} - {pet.Breed}) | {pet.Owner.Name}";
            OnPropertyChanged(nameof(PetInfo));

            allRecords = records;
            FilterRecords(null);
        }

        private void FilterRecords(Object obj)
        {
            var filtered = allRecords.AsEnumerable();

            if (FromDate.HasValue)
                filtered = filtered.Where(r => r.Appointment.Date >= FromDate.Value);

            if (ToDate.HasValue)
                filtered = filtered.Where(r => r.Appointment.Date <= ToDate.Value);

            Application.Current.Dispatcher.Invoke(() =>
            {
                MedicalRecords.Clear();
                foreach (var r in filtered.OrderBy(r => r.Appointment.Date))
                    MedicalRecords.Add(r);
            });
        }
    }
}
