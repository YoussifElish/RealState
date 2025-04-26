namespace RealState.Entities;

public class ContactLeads
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public string? ApplicationUserId { get; set; }
    public bool IsDone { get; set; }
}
