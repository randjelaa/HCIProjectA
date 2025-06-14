using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VetClinic.Models;


namespace VetClinic.Views
{
    /// <summary>
    /// Interaction logic for VetView.xaml
    /// </summary>
    public partial class VetView : Window
    {
        public VetView(User loggedInUser)
        {
            InitializeComponent();
        }
    }
}
