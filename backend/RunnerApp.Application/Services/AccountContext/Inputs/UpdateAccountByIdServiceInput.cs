using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AccountContext.Inputs;

public readonly struct UpdateAccountByIdServiceInput
{
    public string? FirstName { get; }
    public string? Surname { get; }
    public string? Email { get; }

    private UpdateAccountByIdServiceInput(string? firstName, string? surname, string? email)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
    }

    public static UpdateAccountByIdServiceInput Factory(string? firstName, string? surname, string? email)
        => new UpdateAccountByIdServiceInput(firstName, surname, email);
}
