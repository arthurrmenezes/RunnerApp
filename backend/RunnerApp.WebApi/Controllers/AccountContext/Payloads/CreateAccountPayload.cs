namespace RunnerApp.WebApi.Controllers.AccountContext.Payloads;

public readonly struct CreateAccountPayload
{
    public string FirstName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }

    public CreateAccountPayload(string firstName, string surname, string email)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
    }
}
