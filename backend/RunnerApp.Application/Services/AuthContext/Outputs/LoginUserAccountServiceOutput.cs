namespace RunnerApp.Application.Services.AuthContext.Outputs;

public readonly struct LoginUserAccountServiceOutput
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    private LoginUserAccountServiceOutput(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public static LoginUserAccountServiceOutput Factory(string accessToken, string refreshToken)
        => new LoginUserAccountServiceOutput(accessToken, refreshToken);
}
