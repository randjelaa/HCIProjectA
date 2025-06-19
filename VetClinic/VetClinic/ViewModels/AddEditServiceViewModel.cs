using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Resources;
using VetClinic.Views.Windows;


namespace VetClinic.ViewModels
{
    public class AddEditServiceViewModel : BaseViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationMinutes { get; set; }

        public ICommand SaveCommand { get; }

        private readonly Service? originalService;

        public AddEditServiceViewModel(Service? service = null)
        {
            originalService = service;

            if (service != null)
            {
                Name = service.Name;
                Description = service.Description ?? string.Empty;
                Price = service.Price;
                DurationMinutes = service.DurationMinutes ?? 0;
            }

            SaveCommand = new RelayCommand(_ => Save());
        }

        private void Save()
        {
            using var db = new VetClinicContext();

            if (originalService == null)
            {
                var newService = new Service
                {
                    Name = Name,
                    Description = Description,
                    Price = Price,
                    DurationMinutes = DurationMinutes
                };
                db.Services.Add(newService);
            }
            else
            {
                var service = db.Services.Find(originalService.Id);
                if (service == null) return;

                service.Name = Name;
                service.Description = Description;
                service.Price = Price;
                service.DurationMinutes = DurationMinutes;
            }

            db.SaveChanges();

            MessageBox.Show(
                StringResources.SaveSuccessMessage,
                StringResources.SuccessTitle,
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            // Close the dialog
            foreach (Window window in Application.Current.Windows)
            {
                if (window is AddEditServiceWindow)
                {
                    window.DialogResult = true;
                    window.Close();
                    break;
                }
            }
        }
    }
}
