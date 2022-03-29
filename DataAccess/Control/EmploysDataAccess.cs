using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataAccess.Control
{
    public class EmploysDataAccess
    {
        // ctor
        public EmploysDataAccess()
        {
            ReadEmploys();
        }

        // Property
        private ObservableCollection<Employs> Employs { get; } = new ObservableCollection<Employs>();

        // Methods

        #region Methods

        private void ReadEmploys()
        {
            #region employ Test

            Employs employs1 = new Employs
            {
                Id = 1,
                FristName = "amir",
                LastName = "ehsani",
                Address = "Mashad",
                BaseSalary = 150000,
                Department = Department.Advertisement,
                PhoneNumber = "09308505480"
            };
            Employs employs2 = new Employs
            {
                Id = 2,
                FristName = "Hossein",
                LastName = "Massoum Beygi",
                Address = "Mashad",
                BaseSalary = 150000,
                Department = Department.Management,
                PhoneNumber = "09308505480"
            };
            Employs employs3 = new Employs
            {
                Id = 3,
                FristName = "Sobhan",
                LastName = "Massoum Beygi",
                Address = "Mashad",
                BaseSalary = 150000,
                Department = Department.Sales,
                PhoneNumber = "09308505480"
            };
            Employs.Add(employs1);
            Employs.Add(employs2);
            Employs.Add(employs3);

            #endregion
        }

        public bool AddEmployy(Employs employ)
        {
            try
            {
                int id = GetNextId();
                employ.Id = id;
                Employs.Add(employ);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private int GetNextId()
        {
            return Employs.Any() ? Employs.Max(p => p.Id) + 1 : 1;
        }

        public bool RemoveEmploy(int id)
        {
            bool result = false;

            try
            {
                Employs employ = Employs.First(p => p.Id == id);
                result = Employs.Remove(employ);
            }
            catch (NullReferenceException e)
            {
                return result;
            }

            return result;
        }

        public bool EditEmploy(Employs employ)
        {
            try
            {
                Employs pro = Employs.First(p => p.Id == employ.Id);
                pro = employ;
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public ObservableCollection<Employs> GetEmploysCollection()
        {
            return Employs;
        }

        #endregion
    }
}