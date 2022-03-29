using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataAccess.Models;

namespace DataAccess.Control
{
    public class ProductDataAccess
    {
        // ctor
        public ProductDataAccess()
        {
            ReadProducts();
        }

        // Property
        private ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        // Methods

        #region Methods

        private void ReadProducts()
        {
            #region Product Test

            Product p1 = new Product
            {
                Id = 1, Name = "Become Strong In Lonely World", Author = "Amb", Price = 100, BookInventoryCount = 1000
            };

            Product p2 = new Product
            {
                Id = 2, Name = "Don't Weak or people Eat You", Author = "Amb", Price = 200, BookInventoryCount = 1000
            };
            Product p3 = new Product
            {
                Id = 3, Name = "Become Monarch", Author = "Amb", Price = 500, BookInventoryCount = 1000
            };
            Products.Add(p1);
            Products.Add(p2);
            Products.Add(p3);

            #endregion
        }

        public bool AddProduct(Product product)
        {
            try
            {
                int id = GetNextId();
                product.Id = id;
                Products.Add(product);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private int GetNextId()
        {
            return Products.Any() ? Products.Max(p => p.Id) + 1 : 1;
        }

        public bool RemoveProduct(int id)
        {
            bool result = false;
            Product product = Products.FirstOrDefault(p => p.Id == id);
            try
            {
                if (product.Id == id) result = Products.Remove(product);
            }
            catch (NullReferenceException)
            {
                return result;
            }
            catch (Exception)
            {
                return result;
            }

            return result;
        }

        public bool EditProduct(Product product)
        {
            try
            {
                Product pro = Products.First(p => p.Id == product.Id);
                pro = product;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ObservableCollection<Product> GetProductsCollection()
        {
            return Products;
        }

        #endregion
    }
}
//public bool AddProduct(Product product)
//{
//    if (Products.Any(p=>p.Id==product.Id))
//    {
//        return false;
//    }
//    else
//    {
//        Products.Add(product);
//        return true;
//    }
//}