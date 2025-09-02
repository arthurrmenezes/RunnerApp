using System.Globalization;

namespace RunnerApp.Domain.ValueObjects;

public readonly struct FirstNameValueObject
{
    private readonly string FirstName { get; }

    public const int MinLength = 3;
    public const int MaxLength = 64;

    private FirstNameValueObject(string firstName)
    {
        FirstName = firstName;
    }

    public static FirstNameValueObject Factory(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException("First name cannot be null or empty.", nameof(firstName));

        if (firstName.Length < MinLength)
            throw new ArgumentException($"First name must be at least {MinLength} character long.", nameof(firstName));

        if (firstName.Length > MaxLength)
            throw new ArgumentException($"First name cannot exceed {MaxLength} characters.", nameof(firstName));

        string formattedFirstName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(firstName.ToLowerInvariant());

        return new FirstNameValueObject(formattedFirstName);
    }

    public override string ToString() => FirstName;

    public static implicit operator string(FirstNameValueObject firstName) => firstName.FirstName;
    public static implicit operator FirstNameValueObject(string firstName) => Factory(firstName);
}
