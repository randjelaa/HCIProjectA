using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views.Windows;

namespace VetClinic.ViewModels
{
    public class PetsViewModel : BaseViewModel
    {
        public ObservableCollection<Pet> Pets { get; set; } = new();
        public ObservableCollection<Pet> FilteredPets { get; set; } = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                Search();
            }
        }
        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }

        public ICommand ViewRecordsCommand { get; }
        public Pet SelectedPet { get; set; }

        public bool HasPets => FilteredPets.Any();

        public PetsViewModel()
        {
            ViewRecordsCommand = new RelayCommand(ViewRecords);
            SearchCommand = new RelayCommand(_ => Search());
            ClearSearchCommand = new RelayCommand(_ => ClearSearch());
            LoadPets();
        }

        private void LoadPets()
        {
            using var db = new VetClinicContext();
            var pets = db.Pets
                         .Where(p => p.Deleted == null)
                         .ToList();

            foreach (var pet in pets)
                db.Entry(pet).Reference(p => p.Owner).Load();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Pets.Clear();
                FilteredPets.Clear();
                foreach (var pet in pets)
                {
                    Pets.Add(pet);
                    FilteredPets.Add(pet);
                }
                OnPropertyChanged(nameof(HasPets));
            });
        }

        private void Search()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredPets.Clear();
                foreach (var pet in Pets)
                    FilteredPets.Add(pet);
                OnPropertyChanged(nameof(HasPets));
                return;
            }

            var lower = SearchText.ToLower();
            var filtered = Pets.Where(p =>
                (!string.IsNullOrEmpty(p.Name) && p.Name.ToLower().Contains(lower)) ||
                (!string.IsNullOrEmpty(p.Species) && p.Species.ToLower().Contains(lower)) ||
                (!string.IsNullOrEmpty(p.Breed) && p.Breed.ToLower().Contains(lower)) ||
                (!string.IsNullOrEmpty(p.Owner?.Name) && p.Owner.Name.ToLower().Contains(lower))
            ).ToList();

            FilteredPets.Clear();
            foreach (var pet in filtered)
                FilteredPets.Add(pet);
            OnPropertyChanged(nameof(HasPets));
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            Search();
        }

        private void ViewRecords(object obj)
        {
            if (obj is Pet pet)
            {
                var window = new MedicalRecordsWindow(pet);
                window.ShowDialog();
            }
        }
    }

}
