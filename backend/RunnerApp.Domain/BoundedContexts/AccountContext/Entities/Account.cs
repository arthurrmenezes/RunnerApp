using RunnerApp.Domain.BoundedContexts.TrainingContext.Entities;
using RunnerApp.Domain.ValueObjects;

namespace RunnerApp.Domain.BoundedContexts.AccountContext.Entities;

public class Account
{
    public IdValueObject Id { get; private set; }
    public FirstNameValueObject FirstName { get; private set; }
    public SurnameValueObject Surname { get; private set; }
    public EmailValueObject Email { get; private set; }
    public string? ProfilePictureUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public virtual ICollection<Training> Trainings { get; private set; }

    private Account()
    {
        Trainings = new List<Training>();
    }

    private Account(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email, string? profilePictureUrl)
    {
        Id = IdValueObject.New();
        FirstName = FirstNameValueObject.Factory(firstName);
        Surname = SurnameValueObject.Factory(surname);
        Email = EmailValueObject.Factory(email);
        CreatedAt = DateTime.UtcNow;
        Trainings = new List<Training>();
        ProfilePictureUrl = profilePictureUrl;
    }

    public static Account Factory(FirstNameValueObject firstName, SurnameValueObject surname, EmailValueObject email, string? profilePictureUrl)
        => new Account(firstName, surname, email, profilePictureUrl);

    public void UpdateAccountDetails(string? firstName, string? surname, string? email, string? profilePictureUrl)
    {
        if (!string.IsNullOrEmpty(firstName))
            FirstName = FirstNameValueObject.Factory(firstName);

        if (!string.IsNullOrEmpty(surname))
            Surname = SurnameValueObject.Factory(surname);

        if (!string.IsNullOrEmpty(email))
            Email = EmailValueObject.Factory(email);

        if (!string.IsNullOrEmpty(profilePictureUrl))
            ProfilePictureUrl = profilePictureUrl;
    }

    public void SetProfilePicture(string profilePictureUrl)
    {
        if (string.IsNullOrEmpty(profilePictureUrl))
            throw new ArgumentException("Profile picture URL cannot be null or empty.", nameof(profilePictureUrl));

        ProfilePictureUrl = profilePictureUrl;
    }

    public void RemoveProfilePicture()
    {
        ProfilePictureUrl = null;
    }
}
