using Mapster;
using Microsoft.EntityFrameworkCore;
using RealState.Abstactions;
using RealState.Contracts.ContactLead;
using RealState.Entities;
using RealState.Errors;
using RealState.Persistence;

namespace RealState.Services;

public class ContactLeadService(ApplicationDbContext context) : IContactLeadService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> AddContactLead(ContactLeadRequest request, CancellationToken cancellationToken = default)
    {
        var lead = request.Adapt<ContactLeads>();
        _context.contactLeads.Add(lead);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<ContactLeadResponse>> AssignToEmployee(int id, string employeeId, CancellationToken cancellationToken = default)
    {
        var lead = await _context.contactLeads.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (lead is null)
            return Result.Failure<ContactLeadResponse>(LeadErrors.LeadNotFound);

        var employee = await _context.Users.FirstOrDefaultAsync(u => u.Id == employeeId, cancellationToken);
        if (employee is null)
            return Result.Failure<ContactLeadResponse>(LeadErrors.InvalidUserId);

        lead.ApplicationUserId = employee.Id;
        _context.contactLeads.Update(lead);
        await _context.SaveChangesAsync(cancellationToken);

        var response = lead.Adapt<ContactLeadResponse>();
        response.Name = $"{employee.FirstName} {employee.LastName}";

        return Result.Success(response);
    }

    public async Task<Result<List<ContactLeadResponse>>> GetAllContactLead(CancellationToken cancellationToken = default)
    {
        var leads = await _context.contactLeads
            .Include(c => c.ApplicationUser)
            .ToListAsync(cancellationToken);

        var response = leads.Adapt<List<ContactLeadResponse>>();
        for (int i = 0; i < leads.Count; i++)
        {
            response[i].Name = leads[i].ApplicationUser is not null
                ? $"{leads[i].ApplicationUser.FirstName} {leads[i].ApplicationUser.LastName}"
                : null;
        }

        return Result.Success(response);
    }

    public async Task<Result<ContactLeadResponse>> GetContactLead(int id, CancellationToken cancellationToken = default)
    {
        var lead = await _context.contactLeads
            .Include(c => c.ApplicationUser)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (lead is null)
            return Result.Failure<ContactLeadResponse>(LeadErrors.LeadNotFound);

        var response = lead.Adapt<ContactLeadResponse>();
        response.Name = lead.ApplicationUser is not null
            ? $"{lead.ApplicationUser.FirstName} {lead.ApplicationUser.LastName}"
            : null;

        return Result.Success(response);
    }

    public async Task<Result<ContactLeadResponse>> MarkAsDone(int id, CancellationToken cancellationToken = default)
    {
        var lead = await _context.contactLeads.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (lead is null)
            return Result.Failure<ContactLeadResponse>(LeadErrors.LeadNotFound);

        lead.IsDone = true;
        _context.contactLeads.Update(lead);
        await _context.SaveChangesAsync(cancellationToken);

        var response = lead.Adapt<ContactLeadResponse>();
        return Result.Success(response);
    }
}
