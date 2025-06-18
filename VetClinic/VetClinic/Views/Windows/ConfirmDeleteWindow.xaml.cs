using System.Windows;

namespace VetClinic.Views.Windows
{
    public partial class ConfirmDeleteWindow : Window
    {
        public bool Confirmed { get; private set; } = false;

        public ConfirmDeleteWindow()
        {
            InitializeComponent();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = true;
            DialogResult = true;
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Confirmed = false;
            DialogResult = false;
        }
    }
}
