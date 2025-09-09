namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct CreateAccountServiceOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    private CreateAccountServiceOutput(string id, string firstName, string surname, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }

    public static CreateAccountServiceOutput Factory(string id, string firstName, string surname, string email, DateTime createdAt)
        => new CreateAccountServiceOutput(id, firstName, surname, email, createdAt);
}
