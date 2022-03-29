using DataAccess.Models;

namespace DataAccess
{
    public class Employs : IPerson
    {
        public Department Department { get; set; }
        public decimal BaseSalary { get; set; }
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string GetBasicInfo()
        {
            string finalInfo = FristName + " " + LastName + "\nTell : " + PhoneNumber + "\nAdress :" + Address +
                               "\nDepartment : " + Department + "\nBaseSalary : " + BaseSalary;
            return finalInfo;
        }
    }

    public enum Department
    {
        Production,
        Sales,
        Advertisement,
        Management
    }
}