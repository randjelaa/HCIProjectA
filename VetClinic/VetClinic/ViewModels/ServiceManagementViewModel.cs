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
        public ObservableCollection<Service> Services { get; set; }

        public ICommand AddServiceCommand { get; }
        public ICommand EditServiceCommand { get; }
        public ICommand DeleteServiceCommand { get; }

        public ServicesManagementViewModel()
        {
            Services = new ObservableCollection<Service>();
            LoadServices();

            AddServiceCommand = new RelayCommand(_ => ShowServiceDialog());
            EditServiceCommand = new RelayCommand(service => ShowServiceDialog(service as Service));
            DeleteServiceCommand = new RelayCommand(serviceObj =>
            {
                if (serviceObj is Service service)
                {
                    DeleteService(service);
                }
            });
        }

        private void LoadServices()
        {
            using var db = new VetClinicContext();
            var list = db.Services.ToList();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Services.Clear();
                foreach (var s in list)
                    Services.Add(s);
            });
        }

        private void DeleteService(Service service)
        {
            if (service == null) return;

            using var db = new VetClinicContext();
            var existing = db.Services.Find(service.Id);
            if (existing == null) return;

            db.Services.Remove(existing);
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
