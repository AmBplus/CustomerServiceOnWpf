using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.Control
{
    public class CustomerDataAccess
    {
        // ctor
        public CustomerDataAccess()
        {
            ReadCustomer();
        }

        // Property
        public ObservableCollection<Customer> Customer { get; set; } = new ObservableCollection<Customer>();
        // Methods

        #region Methods

        private void ReadCustomer()
        {
            #region customer Test

            Customer Customer1 = new Customer
            {
                Id = 1,
                FristName = "amir",
                LastName = "ehsani",
                Address = "Mashad",
                PhoneNumber = "0930254520"
            };
            Customer Customer2 = new Customer
            {
                Id = 2,
                FristName = "Hossein",
                LastName = "Massoum Beygi",
                Address = "Mashad",

                PhoneNumber = "09308505480"
            };
            Customer Customer3 = new Customer
            {
                Id = 3,
                FristName = "Sobhan",
                LastName = "Massoum Beygi",
                Address = "Mashad",

                PhoneNumber = "09308505480"
            };
            Customer.Add(Customer1);
            Customer.Add(Customer2);
            Customer.Add(Customer3);

            #endregion
        }

        public bool Addcustomer(Customer customer)
        {
            try
            {
                int id = getNextID();
                customer.Id = id;
                Customer.Add(customer);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private int getNextID()
        {
            return Customer.Any() ? Customer.Max(p => p.Id) + 1 : 1;
        }

        public bool Removecustomer(int id)
        {
            bool result = false;

            try
            {
                Customer customer = Customer.First(p => p.Id == id);
                result = Customer.Remove(customer);
            }
            catch (NullReferenceException e)
            {
                return result;
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        public bool Editcustomer(Customer customer)
        {
            try
            {
                Customer pro = Customer.First(p => p.Id == customer.Id);
                pro = customer;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ObservableCollection<Customer> GetCustomersCollection()
        {
            return Customer;
        }

        #endregion
    }
}