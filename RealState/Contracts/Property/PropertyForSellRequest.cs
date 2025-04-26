
using RealState.Entities;

namespace RealState.Contracts.Property;

public class PropertyForSellRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public PropertySellType PropertyType { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }
    public int NumberOfRooms { get; set; }
    public Condition Condition { get; set; }
    public bool IsAvailable { get; set; }
    public int? LaunchId { get; set; }
    public string ContactNumber { get; set; }


}
