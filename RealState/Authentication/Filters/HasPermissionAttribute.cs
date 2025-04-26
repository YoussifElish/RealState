using Microsoft.AspNetCore.Authorization;

namespace RealState.Authentication.Filters
{
    public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
    {

    }
}
