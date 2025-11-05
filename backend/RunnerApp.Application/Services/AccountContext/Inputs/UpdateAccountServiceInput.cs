namespace RunnerApp.Application.Services.AccountContext.Inputs;

public readonly struct UpdateAccountServiceInput
{
    public string? FirstName { get; }
    public string? Surname { get; }
    public string? Email { get; }
    public string? ProfilePictureUrl { get; }

    private UpdateAccountServiceInput(string? firstName, string? surname, string? email, string? profilePictureUrl)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;
    }

    public static UpdateAccountServiceInput Factory(string? firstName, string? surname, string? email, string? profilePictureUrl)
        => new UpdateAccountServiceInput(firstName, surname, email, profilePictureUrl);
}
