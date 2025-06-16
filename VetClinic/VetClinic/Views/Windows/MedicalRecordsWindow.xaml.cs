using System.Windows;
using VetClinic.Models;
using VetClinic.ViewModels;

namespace VetClinic.Views.Windows
{
    public partial class MedicalRecordsWindow : Window
    {
        public MedicalRecordsWindow(Pet pet)
        {
            InitializeComponent();
            DataContext = new MedicalRecordsViewModel(pet);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
