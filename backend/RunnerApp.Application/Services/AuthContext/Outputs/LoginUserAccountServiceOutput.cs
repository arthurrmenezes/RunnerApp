namespace RunnerApp.Application.Services.AuthContext.Outputs;

public readonly struct LoginUserAccountServiceOutput
{
    public string AccessToken { get; }

    private LoginUserAccountServiceOutput(string accessToken)
    {
        AccessToken = accessToken;
    }

    public static LoginUserAccountServiceOutput Factory(string accessToken)
        => new LoginUserAccountServiceOutput(accessToken);
}
