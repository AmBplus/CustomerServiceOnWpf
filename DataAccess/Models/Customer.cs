namespace DataAccess.Models
{
    public class Customer : IPerson
    {
        public int Id { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string GetBasicInfo()
        {
            string finalInfo = FristName + " " + LastName + "\nTell : " + PhoneNumber + "\nAdress :" + Address;
            return finalInfo;
        }
    }
}