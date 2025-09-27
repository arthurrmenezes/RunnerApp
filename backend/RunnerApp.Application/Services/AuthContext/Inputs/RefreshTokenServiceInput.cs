namespace RunnerApp.Application.Services.AuthContext.Inputs;

public readonly struct RefreshTokenServiceInput
{
    public string RefreshToken { get; }

    private RefreshTokenServiceInput(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public static RefreshTokenServiceInput Factory(string refreshToken) =>
        new RefreshTokenServiceInput(refreshToken);
}
