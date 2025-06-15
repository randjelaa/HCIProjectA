using System.Windows;
using VetClinic.Models;
using VetClinic.ViewModels;
using VetClinic.Util;
using System.Linq;

namespace VetClinic.Views
{
    public partial class AdminView : Window
    {
        public AdminView(User loggedInUser)
        {
            // Read preferred language before InitializeComponent
            using var db = new VetClinicContext();
            var pref = db.Userpreferences.FirstOrDefault(p => p.UserId == loggedInUser.Id);
            if (pref?.Language is string lang)
            {
                App.ChangeCulture(lang); // before UI loads
            }

            InitializeComponent();
            var vm = new AdminViewModel(loggedInUser);
            DataContext = vm;
            vm.FrameRef = MainFrame;
            MainFrame.Navigate(new Pages.UserManagementPage());
        }
    }
}
