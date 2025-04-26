using RealState.Entities;

namespace RealState.Contracts.ContactLead;

public class ContactLeadRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
   
}
