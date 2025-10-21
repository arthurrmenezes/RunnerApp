namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct GetUserAccountDetailsServiceOutput
{
    public string Id { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    private GetUserAccountDetailsServiceOutput(string id, string firstName, string surname, string email, DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }

    public static GetUserAccountDetailsServiceOutput Factory(string id, string firstName, string surname, string email, DateTime createdAt)
        => new GetUserAccountDetailsServiceOutput(id, firstName, surname, email, createdAt);
}
