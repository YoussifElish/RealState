using RealState.Abstactions;
using RealState.Contracts.ContactLead;

namespace RealState.Services;

public interface IContactLeadService
{
    Task<Result> AddContactLead(ContactLeadRequest request, CancellationToken cancellationToken = default!);
    Task<Result<ContactLeadResponse>> GetContactLead(int Id, CancellationToken cancellationToken = default!);
    Task<Result<ContactLeadResponse>> AssignToEmployee(int Id,string EmployeeId, CancellationToken cancellationToken = default!);
    Task<Result<ContactLeadResponse>> MarkAsDone(int Id, CancellationToken cancellationToken = default!);
    Task<Result<List<ContactLeadResponse>>> GetAllContactLead( CancellationToken cancellationToken = default!);
}
