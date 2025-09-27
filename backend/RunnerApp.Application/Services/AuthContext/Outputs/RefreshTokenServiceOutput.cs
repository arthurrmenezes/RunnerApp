namespace RunnerApp.Application.Services.AuthContext.Outputs;

public readonly struct RefreshTokenServiceOutput
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    private RefreshTokenServiceOutput(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static RefreshTokenServiceOutput Factory(string accessToken, string refreshToken)
        => new RefreshTokenServiceOutput(accessToken, refreshToken);
}
