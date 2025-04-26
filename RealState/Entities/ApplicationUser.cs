using RealState.Entities;
using Microsoft.AspNetCore.Identity;

namespace RealState.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateOnly DOB { get; set; }
    public bool IsDisabled { get; set; } = false;
    public List<RefreshTokens> RefreshTokens { get; set; } = [];
}

