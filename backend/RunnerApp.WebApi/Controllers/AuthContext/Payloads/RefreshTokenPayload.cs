namespace RunnerApp.WebApi.Controllers.AuthContext.Payloads;

public class RefreshTokenPayload
{
    public string RefreshToken { get; init; }

    public RefreshTokenPayload(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
