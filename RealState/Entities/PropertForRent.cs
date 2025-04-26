namespace RealState.Entities.PropertForRent;
public enum PropertyRentType
{
    Apartment = 1, // شقة
    Villa = 2, // فيلا
    House = 3, // منزل
    Office = 4, // مكتب
    Commercial = 5, // تجاري
    Land = 6 // أرض
}

public enum RentType
{
    Monthly,
    Yearly
}



public class PropertForRent
{
    public int Id { get; set; }

    public string Title { get; set; } 

    public string Description { get; set; } 
    public PropertyRentType PropertyType { get; set; } 

    public decimal RentPrice { get; set; } 

    public string Location { get; set; }

    public DateTime DateListed { get; set; } = DateTime.UtcNow;

    public int NumberOfRooms { get; set; } 


    public bool IsAvailable { get; set; } 

    public DateTime LeaseStartDate { get; set; } 

    public DateTime LeaseEndDate { get; set; }

    public RentType RentType { get; set; } 

    public decimal? DepositAmount { get; set; } 

}