namespace RunnerApp.Application.Services.AuthContext.Outputs;

public readonly struct RegisterUserAccountServiceOutput
{
    public string AccountId { get; }
    public string FirstName { get; }
    public string Surname { get; }
    public string Email { get; }
    public DateTime CreatedAt { get; }

    private RegisterUserAccountServiceOutput(string accountId, string firstName, string surname, string email, DateTime createdAt)
    {
        AccountId = accountId;
        FirstName = firstName;
        Surname = surname;
        Email = email;
        CreatedAt = createdAt;
    }

    public static RegisterUserAccountServiceOutput Factory(string accountId, string firstName, string surname, string email, DateTime createdAt)
        => new RegisterUserAccountServiceOutput(accountId, firstName, surname, email, createdAt);
}
