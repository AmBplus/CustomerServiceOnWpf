using System;
using System.Windows;
using DataAccess;
using DataAccess.Control;

namespace WpfCustomerService.Forms
{
    /// <summary>
    ///     Interaction logic for AddEditDeleteEmploysForm.xaml
    /// </summary>
    public partial class AddEditDeleteEmploysForm : Window
    {
        private readonly Employs _employ;
        private readonly EmploysDataAccess _employsDataAccess;
        private readonly bool flag;

        // ctor ...
        public AddEditDeleteEmploysForm(EmploysDataAccess employs)
        {
            InitializeComponent();
            _employsDataAccess = employs;
            flag = false;
        }

        public AddEditDeleteEmploysForm(EmploysDataAccess employs, Employs epm)
        {
            InitializeComponent();
            _employsDataAccess = employs;
            _employ = epm;
            flag = true;

            #region Fill Field of Form From Sended Employ

            TxtFirstNameEmploy.Text = epm.FristName;
            TxtLastNameEmploy.Text = epm.LastName;
            TxtBaseSalaryEmploy.Text = epm.BaseSalary.ToString();
            TxtAddressEmploy.Text = epm.Address;
            TxtPhoneNumberEmploy.Text = epm.PhoneNumber;
            ComboBoxEmploy.SelectedIndex = (int)epm.Department;

            #endregion
        }

        // Methods ...
        private void BtnCancelEmploy_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOkEmploy_OnClick(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                Employs emp = new Employs();
                emp.FristName = TxtFirstNameEmploy.Text;
                emp.LastName = TxtLastNameEmploy.Text;
                emp.Address = TxtAddressEmploy.Text;
                emp.PhoneNumber = TxtPhoneNumberEmploy.Text;
                emp.Id = _employ.Id;
                emp.Department = (Department)ComboBoxEmploy.SelectedIndex;
                try
                {
                    emp.BaseSalary = Convert.ToDecimal(TxtBaseSalaryEmploy.Text);
                    try
                    {
                        if (_employsDataAccess.EditEmploy(emp))
                        {
                            MessageBox.Show("Edit Employ Successfully ...", "Operation Done ...", MessageBoxButton.OK,
                                MessageBoxImage.Information);
                            Close();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(" Can't Added ...", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(" Format enter For Data Is Uncorect", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else
            {
                Employs employs = new Employs();
                employs.FristName = TxtFirstNameEmploy.Text;
                employs.LastName = TxtLastNameEmploy.Text;
                employs.Address = TxtAddressEmploy.Text;
                employs.PhoneNumber = TxtPhoneNumberEmploy.Text;

                employs.Department = (Department)ComboBoxEmploy.SelectedIndex;
                try
                {
                    employs.BaseSalary = Convert.ToDecimal(TxtBaseSalaryEmploy.Text);
                    try
                    {
                        _employsDataAccess.AddEmployy(employs);
                        MessageBox.Show("Add Employ Successfully ...", "Operation Done ...", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                        Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(" Can't Added ...", "Error", MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(" Format enter For Data Is Uncorect", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}