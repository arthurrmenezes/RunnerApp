using System.Globalization;

namespace RunnerApp.Domain.ValueObjects;

public readonly struct SurnameValueObject
{
    private string Surname { get; }

    public const int MinLength = 3;
    public const int MaxLength = 64;

    private SurnameValueObject(string surname)
    {
        Surname = surname;
    }

    public static SurnameValueObject Factory(string surname)
    {
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentException("Surname cannot be null or empty.", nameof(surname));

        if (surname.Length < MinLength)
            throw new ArgumentException($"First name must be at least {MinLength} character long.", nameof(surname));

        if (surname.Length > MaxLength)
            throw new ArgumentException($"Surname name cannot exceed {MaxLength} characters.", nameof(surname));

        string trimmedSurname = surname.Trim();
        string formattedSurnameName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(trimmedSurname.ToLower());

        return new SurnameValueObject(formattedSurnameName);
    }

    public string GetSurname()
    {
        return Surname;
    }

    public override string ToString() => Surname;

    public static implicit operator string(SurnameValueObject surname) => surname.GetSurname();
    public static implicit operator SurnameValueObject(string surname) => Factory(surname);
}
