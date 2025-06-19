using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using VetClinic.Models;
using VetClinic.Views.Windows;

namespace VetClinic.ViewModels
{
    public class ReportsViewModel : BaseViewModel
    {
        public ObservableCollection<Payment> Payments { get; set; } = new();
        public ObservableCollection<Unpaidservice> UnpaidServices { get; set; } = new();

        public SeriesCollection IncomeSeries { get; set; } = new();
        public string[] IncomeLabels { get; set; } = Array.Empty<string>();

        public SeriesCollection PopularServices { get; set; } = new();

        public ICommand ShowUserCommand { get; }

        public bool HasPayments => Payments.Any();
        public bool HasUnpaidServices => UnpaidServices.Any();

        public ReportsViewModel()
        {
            LoadData();

            ShowUserCommand = new Command<Unpaidservice>(s =>
            {
                if (s?.User != null)
                {
                    var window = new UserDetailsWindow
                    {
                        Owner = Application.Current.MainWindow,
                        DataContext = s.User
                    };
                    window.ShowDialog();
                }
            });
        }

        private void LoadData()
        {
            using var db = new VetClinicContext();

            // Payments
            var payments = db.Payments
                .Include(p => p.User)
                .Where(p => p.User.Deleted == null)
                .ToList();

            App.Current.Dispatcher.Invoke(() =>
            {
                Payments.Clear();
                foreach (var p in payments)
                    Payments.Add(p);
                OnPropertyChanged(nameof(HasPayments));
            });

            // Unpaid Services
            var unpaid = db.Unpaidservices
                .Include(p => p.User)
                    .ThenInclude(r => r.Role)
                .Where(p => p.User.Deleted == null)
                .ToList();

            App.Current.Dispatcher.Invoke(() =>
            {
                UnpaidServices.Clear();
                foreach (var s in unpaid)
                    UnpaidServices.Add(s);
                OnPropertyChanged(nameof(HasPayments));
            });

            // Chart: Income over time (group by day)
            var grouped = payments
                .GroupBy(p => p.Date.Date)
                .OrderBy(g => g.Key)
                .ToList();

            var amounts = new ChartValues<decimal>();
            var labels = new List<string>();

            foreach (var group in grouped)
            {
                labels.Add(group.Key.ToShortDateString());
                amounts.Add(group.Sum(p => p.Amount));
            }

            IncomeSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Income",
                    Values = amounts
                }
            };
            IncomeLabels = labels.ToArray();

            // Chart: Most popular services
            var serviceCounts = db.Appointments
                .Include(a => a.Service)
                .Where(a => a.Service.Deleted == null)
                .GroupBy(a => a.Service.Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList();

            var pieSeries = new SeriesCollection();
            foreach (var s in serviceCounts)
            {
                pieSeries.Add(new PieSeries
                {
                    Title = s.Name,
                    Values = new ChartValues<int> { s.Count }
                });
            }

            PopularServices = pieSeries;

            OnPropertyChanged(nameof(IncomeSeries));
            OnPropertyChanged(nameof(IncomeLabels));
            OnPropertyChanged(nameof(PopularServices));
        }
    }
}
