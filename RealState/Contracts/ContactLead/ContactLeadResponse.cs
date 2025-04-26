using RealState.Entities;

namespace RealState.Contracts.ContactLead;

public class ContactLeadResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public string EmployeeName { get; set; }
    public bool IsDone { get; set; }
}
