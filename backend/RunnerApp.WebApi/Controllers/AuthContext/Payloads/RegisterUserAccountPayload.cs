namespace RunnerApp.WebApi.Controllers.AuthContext.Payloads;

public readonly struct RegisterUserAccountPayload
{
    public string FirstName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string RePassword { get; init; }

    public RegisterUserAccountPayload(string firstName, string surname, string email, string password, string rePassword)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
        Password = password;
        RePassword = rePassword;
    }
}
