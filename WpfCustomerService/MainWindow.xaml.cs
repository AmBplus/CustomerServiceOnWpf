using System.Windows;

namespace WpfCustomerService
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnHome_OnClick(object sender, RoutedEventArgs e)
        {
            HomePanel.Visibility = Visibility.Collapsed;
        }

        private void BtnEmploys_OnClick(object sender, RoutedEventArgs e)
        {
            EmploysPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnCustomers_OnClick(object sender, RoutedEventArgs e)
        {
            customerPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnProducts_OnClick(object sender, RoutedEventArgs e)
        {
            ProductsPanel.Visibility = Visibility.Collapsed;
        }
    }
}