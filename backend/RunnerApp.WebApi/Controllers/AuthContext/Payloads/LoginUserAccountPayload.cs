namespace RunnerApp.WebApi.Controllers.AuthContext.Payloads;

public class LoginUserAccountPayload
{
    public string Email { get; init; }
    public string Password { get; init; }

    public LoginUserAccountPayload(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
