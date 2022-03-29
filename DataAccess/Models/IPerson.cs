namespace DataAccess.Models
{
    public interface IPerson
    {
        int Id { get; set; }
        string FristName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Address { get; set; }
        string GetBasicInfo();
    }
}