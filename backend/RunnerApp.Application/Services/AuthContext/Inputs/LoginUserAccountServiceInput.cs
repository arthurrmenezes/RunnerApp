using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AuthContext.Inputs;

public readonly struct LoginUserAccountServiceInput
{
    public EmailValueObject Email { get; }
    public string Password { get; }

    private LoginUserAccountServiceInput(EmailValueObject email, string password)
    {
        Email = EmailValueObject.Factory(email);
        Password = password;
    }

    public static LoginUserAccountServiceInput Factory(EmailValueObject email, string password)
        => new LoginUserAccountServiceInput(email, password);
}
