using System.Windows.Controls;
using VetClinic.ViewModels;

namespace VetClinic.Views.Pages
{
    public partial class ReportsPage : Page
    {
        public ReportsPage()
        {
            InitializeComponent();
            DataContext = new ReportsViewModel();
        }
    }
}
