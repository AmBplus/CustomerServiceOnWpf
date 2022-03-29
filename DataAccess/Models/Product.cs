namespace DataAccess.Models
{
    public class Product : IProduct
    {
        // Field


        // Property
        public int BookInventoryCount { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

        // Ctor
        public string GetBasicInfo()
        {
            string finalStr = Name + "\n Author : " + Author + "\nPrice : " + Price + "$\nAvailable count : " +
                              BookInventoryCount;
            return finalStr;
        }
    }
}