namespace RunnerApp.Application.Services.AuthContext.Inputs;

public readonly struct LogoutServiceInput
{
    public string RefreshToken { get; }

    private LogoutServiceInput(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public static LogoutServiceInput Factory(string refreshToken)
        => new LogoutServiceInput(refreshToken);
}
