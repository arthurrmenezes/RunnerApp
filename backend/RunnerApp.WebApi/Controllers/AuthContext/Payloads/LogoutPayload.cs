namespace RunnerApp.WebApi.Controllers.AuthContext.Payloads;

public class LogoutPayload
{
    public string RefreshToken { get; init; }

    public LogoutPayload(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}
