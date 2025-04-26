namespace RealState.Contracts.Auth;
public record RefreshTokenRequest
    (
    string Token,
    string RefreshToken
    );
