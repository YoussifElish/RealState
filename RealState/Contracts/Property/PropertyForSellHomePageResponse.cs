namespace RealState.Contracts.Property;

public class PropertyForSellHomePageResponse
{
    public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public List<string> Images { get; set; }
}
