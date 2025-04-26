namespace RealState.Contracts.Authentication
{
    public record ConfirmEmailRequest(
        string UserId,
        String Code
        );
}
