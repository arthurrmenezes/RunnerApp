namespace RunnerApp.Application.Services.AuthContext.Outputs;

public readonly struct LoginUserAccountServiceOutput
{
    public string AcessToken { get; }

    private LoginUserAccountServiceOutput(string acessToken)
    {
        AcessToken = acessToken;
    }

    public static LoginUserAccountServiceOutput Factory(string acessToken)
        => new LoginUserAccountServiceOutput(acessToken);
}
