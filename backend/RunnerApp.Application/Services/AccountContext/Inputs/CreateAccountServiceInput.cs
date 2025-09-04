using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Application.Services.AccountContext.Inputs;

public readonly struct CreateAccountServiceInput
{
    public FirstNameValueObject FirstName { get; }
    public SurnameValueObject Surname { get; }
    public EmailValueObject Email { get; }

    private CreateAccountServiceInput(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email)
    {
        FirstName = FirstNameValueObject.Factory(firstName);
        Surname = SurnameValueObject.Factory(surname);
        Email = EmailValueObject.Factory(email);
    }

    public static CreateAccountServiceInput Factory(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email)
        => new CreateAccountServiceInput(firstName, surname, email);
}
