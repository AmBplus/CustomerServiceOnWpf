using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataAccess;
using DataAccess.Control;
using DataAccess.Models;

namespace WpfCustomerService
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Field
        private readonly CustomerDataAccess _customerDataAccess = new CustomerDataAccess();
        private readonly EmploysDataAccess _employsDataAccess = new EmploysDataAccess();
        private readonly ProductDataAccess _productDataAccess = new ProductDataAccess();
        private ObservableCollection<Customer> _customerCollection = new ObservableCollection<Customer>();
        private ObservableCollection<Employs> _employsCollection = new ObservableCollection<Employs>();
        private ObservableCollection<Product> _productsCollection = new ObservableCollection<Product>();

        // ctor
        public MainWindow()
        {
            InitializeComponent();
            HomePanel.Visibility = Visibility.Visible;
#pragma warning disable CS4014
            FillData();
        }
        // Property

        public Employs CurenEmploys { get; set; } = new Employs();
        public Product CurenProduct { get; set; } = new Product();

        public Customer CurenCustomer { get; set; } = new Customer();

        // Methods
        private async Task FillData()
        {
            _productsCollection = _productDataAccess.GetProductsCollection();
            _customerCollection = _customerDataAccess.GetCustomersCollection();
            _employsCollection = _employsDataAccess.GetEmploysCollection();
        }

        private void BtnHome_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("HomePanel");
        }

        private async Task FillDataGrid(string dataGridName)
        {
            //  MessageBox.Show(Task.CurrentId.ToString());

            if (dataGridName == "Employ")
                Task.Run(() =>
                {
                    Dispatcher.BeginInvoke((Action)(() => { DataGridEmploy.ItemsSource = _employsCollection; }));
                });

            else if (dataGridName == "Customer")
                Task.Factory.StartNew(() =>
                {
                    Dispatcher.Invoke((Action)(() => { DataGridCustomer.ItemsSource = _customerCollection; }));
                });

            else if (dataGridName == "Product")
                Task.Run(() =>
                {
                    Dispatcher.Invoke((Action)(() => { DataGridProduct.ItemsSource = _productsCollection; }));
                });
        }

        private async void BtnEmploys_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("EmploysPanel");

            await FillDataGrid("Employ");
        }

        private async void BtnCustomers_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("customerPanel");
            await FillDataGrid("Customer");
        }

        private async void BtnProducts_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("ProductsPanel");
            await FillDataGrid("Product");
        }

        private void CollapsedPanel(string visible)
        {
            HomePanel.Visibility = Visibility.Collapsed;
            EmploysPanel.Visibility = Visibility.Collapsed;
            CustomerPanel.Visibility = Visibility.Collapsed;
            ProductsPanel.Visibility = Visibility.Collapsed;
            if (visible == "HomePanel")
                HomePanel.Visibility = Visibility.Visible;
            else if (visible == "EmploysPanel")
                EmploysPanel.Visibility = Visibility.Visible;
            else if (visible == "customerPanel")
                CustomerPanel.Visibility = Visibility.Visible;
            else if (visible == "ProductsPanel") ProductsPanel.Visibility = Visibility.Visible;
        }


        private void EmployDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEmploy.SelectedIndex >= 0)
            {
                CurenEmploys = DataGridEmploy.SelectedItem as Employs;
                LblEmploy.Content = CurenEmploys?.GetBasicInfo();
            }
        }

        private void BtnAddEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            // // throw new NotImplementedException();
        }

        private void BtnEditEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            // // throw new NotImplementedException();
        }

        private void BtnDeleteEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            // // throw new NotImplementedException();
        }

        private void BtnEditCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BtnAddCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BtnDeleteCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void DataGridProduct_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridProduct.SelectedIndex >= 0)
            {
                CurenProduct = DataGridProduct.SelectedItem as Product;
                lblProduct.Content = CurenProduct?.GetBasicInfo();
            }
        }

        private void DataGridCustomer_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridCustomer.SelectedIndex >= 0)
            {
                CurenCustomer = DataGridCustomer.SelectedItem as Customer;
                LblCustomer.Content = CurenCustomer?.GetBasicInfo();
            }
        }

        private void BtnEditProduct_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BtnDeleteProduct_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BtnAddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            // throw new NotImplementedException();
        }
    }
}