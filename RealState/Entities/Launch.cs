namespace RealState.Entities;

public class Launch
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<PropertForSell>?  propertForSells{ get; set; }
}
