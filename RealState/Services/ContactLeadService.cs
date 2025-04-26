using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealState.Abstactions;
using RealState.Abstactions.Consts;
using RealState.Contracts.ContactLead;
using RealState.Entities;
using RealState.Errors;
using RealState.Persistence;
using System.Security.Claims;

namespace RealState.Services;

public class ContactLeadService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager) : IContactLeadService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Result> AddContactLead(ContactLeadRequest request, CancellationToken cancellationToken = default)
    {
        var lead = request.Adapt<ContactLeads>();
        _context.contactLeads.Add(lead);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<ContactLeadResponse>> AssignToEmployee(int id, string employeeId, CancellationToken cancellationToken = default)
    {

        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userManager.FindByIdAsync(userId);
        var userRoles = await _userManager.GetRolesAsync(user);
        if (!userRoles.Contains("Admin"))
            return Result.Failure<ContactLeadResponse>(LeadErrors.NotAuthorizedToAssignLead);
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
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        var user = await _userManager.FindByIdAsync(userId);
     

        var userRoles = await _userManager.GetRolesAsync(user);

        IQueryable<ContactLeads> query = _context.contactLeads.Include(c => c.ApplicationUser);

        if (!userRoles.Contains(DefaultRoles.Admin))
        {
            query = query.Where(c => c.ApplicationUserId == userId);
        }

        var leads = await query.ToListAsync(cancellationToken);

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
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(userId);
   

        var userRoles = await _userManager.GetRolesAsync(user);

        var lead = await _context.contactLeads
            .Include(c => c.ApplicationUser)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (lead is null)
            return Result.Failure<ContactLeadResponse>(LeadErrors.LeadNotFound);

        // تحقق من الصلاحيات بناءً على الدور
        if (!userRoles.Contains(DefaultRoles.Admin))
        {
            // لو مش Admin
            if (lead.ApplicationUserId != userId)
            {
                return Result.Failure<ContactLeadResponse>(LeadErrors.NotAuthorizedToAccessLead);
            }
        }

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

        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _userManager.FindByIdAsync(userId);
        if (lead.ApplicationUserId != userId)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Contains("Admin"))
                return Result.Failure<ContactLeadResponse>(LeadErrors.NotAuthorizedToMarkAsDone);
        }

        lead.IsDone = true;
        _context.contactLeads.Update(lead);
        await _context.SaveChangesAsync(cancellationToken);

        var response = lead.Adapt<ContactLeadResponse>();
        return Result.Success(response);
    }
}
