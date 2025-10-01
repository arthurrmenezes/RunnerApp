namespace RunnerApp.WebApi.Controllers.AccountContext.Payloads;

public class UpdateAccountPayload
{
    public string? FirstName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }

    public UpdateAccountPayload(string? firstName, string? surname, string? email)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
    }
}
