using Microsoft.AspNetCore.Authorization;

namespace RealState.Authentication.Filters
{
    public class PermissionRequirment(string permission) : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}
