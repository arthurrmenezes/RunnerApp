namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct GetAccountByIdServiceOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    private GetAccountByIdServiceOutput(string id, string firstName, string surname, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }

    public static GetAccountByIdServiceOutput Factory(string id, string firstName, string surname, string email, DateTime createdAt)
        => new GetAccountByIdServiceOutput(id, firstName, surname, email, createdAt);
}
