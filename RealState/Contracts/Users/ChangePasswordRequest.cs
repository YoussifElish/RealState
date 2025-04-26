namespace RealState.Contracts.Users
{
    public record ChangePasswordRequest(

        string CurrentPassword,
        string NewPassword);
}
