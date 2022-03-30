using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataAccess;
using DataAccess.Control;
using DataAccess.Models;
using WpfCustomerService.Forms;

namespace WpfCustomerService
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ctor
        public MainWindow()
        {
            InitializeComponent();
            HomePanel.Visibility = Visibility.Visible;
#pragma warning disable CS4014
            FillData();
        }
        // Field

        #region Field

        private readonly CustomerDataAccess _customerDataAccess = new CustomerDataAccess();
        private readonly EmploysDataAccess _employsDataAccess = new EmploysDataAccess();
        private readonly ProductDataAccess _productDataAccess = new ProductDataAccess();
        private ObservableCollection<Customer> _customerCollection = new ObservableCollection<Customer>();
        private ObservableCollection<Employs> _employsCollection = new ObservableCollection<Employs>();
        private ObservableCollection<Product> _productsCollection = new ObservableCollection<Product>();

        #endregion

        // Property

        #region Property

        private Employs CurenEmploys { get; set; } = new Employs();
        private Product CurenProduct { get; set; } = new Product();

        private Customer CurenCustomer { get; set; } = new Customer();

        #endregion

        // Methods

        #region Aggrigate method _Employ_Product_Customer

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

        #endregion

        #region Employs

        private Employs getSelectedDataGirdEmploy()
        {
            return DataGridEmploy.SelectedItem as Employs;
        }

        private void EmployDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridEmploy.SelectedIndex >= 0)
            {
                CurenEmploys = getSelectedDataGirdEmploy();
                LblEmploy.Content = CurenEmploys?.GetBasicInfo();
            }
        }

        private async void BtnEmploys_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("EmploysPanel");

            await FillDataGrid("Employ");
        }

        private void BtnAddEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            AddEditDeleteEmploysForm addEmloys = new AddEditDeleteEmploysForm(_employsDataAccess);
            addEmloys.ShowDialog();
        }

        private void BtnEditEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridEmploy.SelectedIndex >= 0)
            {
                AddEditDeleteEmploysForm editEmploy =
                    new AddEditDeleteEmploysForm(_employsDataAccess, getSelectedDataGirdEmploy());
                editEmploy.ShowDialog();
            }
        }

        private void BtnDeleteEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridEmploy.SelectedIndex >= 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are You Sure Delete this Emloy ?", "Warning",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.OK)
                {
                    Employs employ = DataGridEmploy.SelectedItem as Employs;
                    if (_employsDataAccess.RemoveEmploy(employ.Id))
                    {
                        MessageBox.Show("Employ Deleted", "Information", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        LblEmploy.Content = " --- ";
                    }
                }
            }
        }

        #endregion

        #region Customer

        private async void BtnCustomers_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("customerPanel");
            await FillDataGrid("Customer");
        }

        private void BtnEditCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridCustomer.SelectedIndex >= 0)
            {
                Customer customer = DataGridCustomer.SelectedItem as Customer;
                AddEditDeleteCustomerForm editCustomer = new AddEditDeleteCustomerForm(_customerDataAccess, customer);
                editCustomer.ShowDialog();
            }
        }

        private void BtnAddCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            AddEditDeleteCustomerForm addCustomer = new AddEditDeleteCustomerForm(_customerDataAccess);
            addCustomer.ShowDialog();
        }

        private void BtnDeleteCustomer_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridCustomer.SelectedIndex >= 0)
            {
                Customer customer = DataGridCustomer.SelectedItem as Customer;
                try
                {
                    MessageBoxResult msBoxResult = MessageBox.Show("Are You Realy Want Delete Seleted Item ?",
                        "Warning ", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (msBoxResult == MessageBoxResult.OK)
                    {
                        bool flag = _customerDataAccess.Removecustomer(customer.Id);
                        if (flag)
                            MessageBox.Show("Successfully Delete Selected Customer ....", "Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        else
                            MessageBox.Show("USuccessfully Delete Selected Customer ....", "Error Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Information", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
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

        #endregion

        #region Products

        private async void BtnProducts_OnClick(object sender, RoutedEventArgs e)
        {
            CollapsedPanel("ProductsPanel");
            await FillDataGrid("Product");
        }

        private void DataGridProduct_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridProduct.SelectedIndex >= 0)
            {
                CurenProduct = DataGridProduct.SelectedItem as Product;
                lblProduct.Content = CurenProduct?.GetBasicInfo();
            }
        }

        private void BtnEditProduct_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridProduct.SelectedIndex >= 0)
            {
                Product product = DataGridProduct.SelectedItem as Product;
                AddEditDeleteProductForm editProduct = new AddEditDeleteProductForm(_productDataAccess, product);
                editProduct.ShowDialog();
            }
        }

        private void BtnDeleteProduct_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridProduct.SelectedIndex >= 0)
            {
                Product product = DataGridProduct.SelectedItem as Product;
                try
                {
                    MessageBoxResult msBoxResult = MessageBox.Show("Are You Realy Want Delete Seleted Item ?",
                        "Warning ", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (msBoxResult == MessageBoxResult.OK)
                    {
                        bool flag = _productDataAccess.RemoveProduct(product.Id);
                        if (flag)
                            MessageBox.Show("Successfully Delete Selected Customer ....", "Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        else
                            MessageBox.Show("USuccessfully Delete Selected Customer ....", "Error Information",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Information", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void BtnAddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            AddEditDeleteProductForm addEditDeleteProductForm = new AddEditDeleteProductForm(_productDataAccess);
            addEditDeleteProductForm.ShowDialog();
        }

        #endregion
    }
}