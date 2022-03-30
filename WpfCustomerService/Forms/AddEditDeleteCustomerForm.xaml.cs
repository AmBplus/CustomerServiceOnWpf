using System;
using System.Windows;
using DataAccess.Control;
using DataAccess.Models;

namespace WpfCustomerService.Forms
{
    /// <summary>
    ///     Interaction logic for AddEditDeleteCustomerForm.xaml
    /// </summary>
    public partial class AddEditDeleteCustomerForm : Window
    {
        private readonly Customer _customerInstance;

        // Field ...
        private readonly CustomerDataAccess customerDataAccess;

        private readonly bool flag;

        // Ctor
        public AddEditDeleteCustomerForm(CustomerDataAccess customerDataAccess)
        {
            InitializeComponent();
            this.customerDataAccess = customerDataAccess;
            flag = false;
        }

        public AddEditDeleteCustomerForm(CustomerDataAccess customerDataAccess, Customer customer)
        {
            InitializeComponent();
            flag = true;
            _customerInstance = customer;
            this.customerDataAccess = customerDataAccess;
            TxtAddressCustomer.Text = customer.Address;
            TxtFirstNameCustomer.Text = customer.FristName;
            TxtLastNameCustomer.Text = customer.LastName;
            TxtPhoneNumberCustomer.Text = customer.PhoneNumber;
        }
        // Methods ...

        private void BtnCancelEmploy_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Customer SetCustomerFromTxtBox()
        {
            try
            {
                Customer customer = new Customer();
                customer.FristName = TxtFirstNameCustomer.Text;
                customer.Address = TxtAddressCustomer.Text;
                customer.PhoneNumber = TxtPhoneNumberCustomer.Text;
                customer.LastName = TxtLastNameCustomer.Text;
                return customer;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void BtnOkEmploy_OnClick(object sender, RoutedEventArgs e)
        {
            Customer? customer;
            if (flag)
            {
                customer = SetCustomerFromTxtBox();
                if (customer != null)
                {
                    customer.Id = _customerInstance.Id;

                    try
                    {
                        if (customerDataAccess.Editcustomer(customer))
                            MessageBox.Show("Coustomer Successfully Edited ...", "Information", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                        else
                            MessageBox.Show("Edit Selected Customer Failed ...", "Error", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Edit Selected Customer Failed ...", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                customer = SetCustomerFromTxtBox();
                if (customer != null)
                    try
                    {
                        customerDataAccess.Addcustomer(customer);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Add Customer Failed ...", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
            }

            Close();
        }
    }
}