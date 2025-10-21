using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Domain.BoundedContexts.AccountContext.Entities;

public class Account
{
    public IdValueObject Id { get; private set; }
    public FirstNameValueObject FirstName { get; private set; }
    public SurnameValueObject Surname { get; private set; }
    public EmailValueObject Email { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public virtual ICollection<Training> Trainings { get; private set; }

    private Account()
    {
        Trainings = new List<Training>();
    }

    private Account(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email)
    {
        Id = IdValueObject.New();
        FirstName = FirstNameValueObject.Factory(firstName);
        Surname = SurnameValueObject.Factory(surname);
        Email = EmailValueObject.Factory(email);
        CreatedAt = DateTime.UtcNow;
        Trainings = new List<Training>();
    }

    public static Account Factory(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email)
        => new Account(firstName, surname, email);

    public void UpdateAccountDetails(string? firstName, string? surname, string? email)
    {
        if (!string.IsNullOrEmpty(firstName))
            FirstName = FirstNameValueObject.Factory(firstName);

        if (!string.IsNullOrEmpty(surname))
            Surname = SurnameValueObject.Factory(surname);

        if (!string.IsNullOrEmpty(email))
            Email = EmailValueObject.Factory(email);
    }
}
