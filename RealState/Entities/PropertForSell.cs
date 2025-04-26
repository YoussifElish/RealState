

namespace RealState.Entities.PropertForSell
{

    public enum PropertySellType
    {
        Apartment = 1, // شقة
        Villa = 2, // فيلا
        House = 3, // منزل
        Office = 4, // مكتب
        Commercial = 5, // تجاري
        Land = 6 // أرض
    }

    public enum Condition
    {
        New = 1, // جديد
        Used = 2, // مستعمل
        UnderConstruction = 3, // تحت الإنشاء
        Renovated = 4 // تم تجديده
    }

    public class PropertForSell
    {
        public int Id { get; set; } 

        public string Title { get; set; }
        public string Description { get; set; }

        public PropertySellType PropertyType { get; set; } 

        public decimal Price { get; set; } 

        public string Location { get; set; }

        public DateTime DateListed { get; set; } = DateTime.UtcNow;

        public int NumberOfRooms { get; set; }

        public Condition Condition { get; set; } 



        public bool IsAvailable { get; set; } 


    }
}
