using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views.Windows;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace VetClinic.ViewModels
{
    public class ServicesManagementViewModel : BaseViewModel
    {
        public ObservableCollection<Service> Services { get; set; } = new();
        public ObservableCollection<Service> FilteredServices { get; set; } = new();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                ApplySearch();
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ClearSearchCommand { get; }
        public ICommand AddServiceCommand { get; }
        public ICommand EditServiceCommand { get; }
        public ICommand DeleteServiceCommand { get; }

        public ServicesManagementViewModel()
        {
            LoadServices();

            SearchCommand = new RelayCommand(_ => ApplySearch());
            ClearSearchCommand = new RelayCommand(_ => ClearSearch());
            AddServiceCommand = new RelayCommand(_ => ShowServiceDialog());
            EditServiceCommand = new RelayCommand(service => ShowServiceDialog(service as Service));
            DeleteServiceCommand = new RelayCommand(serviceObj =>
            {
                if (serviceObj is Service service)
                    DeleteService(service);
            });
        }

        private void ApplySearch()
        {
            var query = SearchText?.ToLower() ?? "";

            var filtered = Services.Where(s =>
                (!string.IsNullOrEmpty(s.Name) && s.Name.ToLower().Contains(query)) ||
                (!string.IsNullOrEmpty(s.Description) && s.Description.ToLower().Contains(query))
            ).ToList();

            FilteredServices.Clear();
            foreach (var service in filtered)
                FilteredServices.Add(service);
        }

        private void ClearSearch()
        {
            SearchText = string.Empty;
            ApplySearch();
        }

        private void LoadServices()
        {
            using var db = new VetClinicContext();
            var list = db.Services
                .Where(s => s.Deleted == null)
                .ToList();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Services.Clear();
                FilteredServices.Clear();
                foreach (var s in list)
                {
                    Services.Add(s);
                    FilteredServices.Add(s);
                }
            });
        }

        private void DeleteService(Service service)
        {
            if (service == null) return;

            using var db = new VetClinicContext();
            var existing = db.Services.Find(service.Id);
            if (existing == null || existing.Deleted != null) return;

            existing.Deleted = DateTime.Now;
            db.SaveChanges();

            LoadServices();
        }

        private void ShowServiceDialog(Service? service = null)
        {
            var window = new AddEditServiceWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = new AddEditServiceViewModel(service)
            };

            if (window.ShowDialog() == true)
                LoadServices();
        }
    }
}
