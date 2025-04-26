using RealState.Entities.PropertForRent;


namespace RealState.Contracts.Property;

public class PropertyForRentResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public PropertyRentType PropertyType { get; set; }
    public decimal RentPrice { get; set; }
    public string Location { get; set; }
    public DateTime DateListed { get; set; }
    public int NumberOfRooms { get; set; }
    public List<string> Images { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime LeaseStartDate { get; set; }
    public DateTime LeaseEndDate { get; set; }
    public RentType RentType { get; set; }
    public decimal DepositAmount { get; set; }
}
