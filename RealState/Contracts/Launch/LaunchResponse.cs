using RealState.Contracts.Property;

namespace RealState.Contracts.Launch;

public class LaunchResponse
{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<string> BannerImages { get; set; }
    public List<string> MasterPlanImages { get; set; }
    public List<string> LocationImages { get; set; }
    public List<string> PaymentPlanImages { get; set; }
    public List<PropertyForSellResponse>? Properties { get; set; }


}
