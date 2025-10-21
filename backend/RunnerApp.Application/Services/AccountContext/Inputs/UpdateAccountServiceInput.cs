namespace RunnerApp.Application.Services.AccountContext.Inputs;

public readonly struct UpdateAccountServiceInput
{
    public string? FirstName { get; }
    public string? Surname { get; }
    public string? Email { get; }

    private UpdateAccountServiceInput(string? firstName, string? surname, string? email)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
    }

    public static UpdateAccountServiceInput Factory(string? firstName, string? surname, string? email)
        => new UpdateAccountServiceInput(firstName, surname, email);
}
