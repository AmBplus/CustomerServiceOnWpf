using System;
using System.Windows;
using DataAccess.Control;
using DataAccess.Models;

namespace WpfCustomerService.Forms
{
    /// <summary>
    ///     Interaction logic for AddEditDeleteProductForm.xaml
    /// </summary>
    public partial class AddEditDeleteProductForm : Window
    {
        // Field ...
        private readonly ProductDataAccess _productDataAccess;
        private readonly Product _productInstance;

        private readonly bool flag;

        // Ctor
        /// <summary>
        ///     Ctor For Add Product Into DataBase
        /// </summary>
        /// <param name="productDataAccess">Enter Instance DataAccess</param>
        public AddEditDeleteProductForm(ProductDataAccess productDataAccess)
        {
            InitializeComponent();
            _productDataAccess = productDataAccess;
            flag = false;
        }

        /// <summary>
        ///     Ctor For Edit Selected Product
        /// </summary>
        /// <param name="productDataAccess">Enter Instance ProductDataAccsess</param>
        /// <param name="product">Enter Seleted Product For Edit</param>
        public AddEditDeleteProductForm(ProductDataAccess productDataAccess, Product product)
        {
            InitializeComponent();
            _productDataAccess = productDataAccess;
            flag = false;
            _productInstance = product;

            #region Set Form Field From Sended product

            TxtAthorProduct.Text = product.Author;
            TxtBookCountInventoryProduct.Text = product.BookInventoryCount.ToString();
            TxtFirstNameProduct.Text = product.Name;
            TxtPriceProduct.Text = product.Price.ToString();

            #endregion
        }

        // Methods
        /// <summary>
        ///     Create an Product From Input Field From TxtBoxForm
        /// </summary>
        /// <returns>Retutn Product If Field From Form Have Valid Value If not Retun Null</returns>
        private Product setProductFromTxtBox()
        {
            Product product = new Product();
            try
            {
                product.Price = Convert.ToDecimal(TxtPriceProduct.Text);
                product.Name = TxtFirstNameProduct.Text;
                product.Author = TxtAthorProduct.Text;
                product.BookInventoryCount = Convert.ToInt32(TxtBookCountInventoryProduct.Text);
                return product;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void BtnCancelProduct_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOkProduct_OnClick(object sender, RoutedEventArgs e)
        {
            Product product;
            if (flag)
                try
                {
                    product = setProductFromTxtBox();
                    if (product != null)
                    {
                        product.Id = _productInstance.Id;
                        _productDataAccess.EditProduct(product);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            else
                try
                {
                    product = setProductFromTxtBox();
                    if (product != null) _productDataAccess.AddProduct(product);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            Close();
        }
    }
}