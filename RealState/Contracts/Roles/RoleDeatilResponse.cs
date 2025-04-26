namespace RealState.Contracts.Roles
{
    public record RoleDeatilResponse(
        string Id,
        string Name,
        bool isDeleted,
        IEnumerable<string> Permissions
        );
}
