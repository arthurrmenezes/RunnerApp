using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AuthContext.Inputs;

public readonly struct RegisterUserAccountServiceInput
{
    public FirstNameValueObject FirstName { get; }
    public SurnameValueObject Surname { get; }
    public EmailValueObject Email { get; }
    public string Password { get; }
    public string RePassword { get; }

    private RegisterUserAccountServiceInput(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email, string password, string rePassword)
    {
        FirstName = FirstNameValueObject.Factory(firstName);
        Surname = SurnameValueObject.Factory(surname);
        Email = EmailValueObject.Factory(email);
        Password = password;
        RePassword = rePassword;
    }

    public static RegisterUserAccountServiceInput Factory(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email, string password, string rePassword)
        => new RegisterUserAccountServiceInput(firstName, surname, email, password, rePassword);
}
