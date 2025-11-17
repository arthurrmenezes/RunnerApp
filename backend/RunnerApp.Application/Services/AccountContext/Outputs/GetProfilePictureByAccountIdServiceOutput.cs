namespace RunnerApp.Application.Services.AccountContext.Outputs;

public sealed class GetProfilePictureByAccountIdServiceOutput
{
    public byte[] ProfilePicture { get; }
    public string FileType { get; }

    public GetProfilePictureByAccountIdServiceOutput(byte[] profilePicture, string fileType)
    {
        ProfilePicture = profilePicture;
        FileType = fileType;
    }

    public static GetProfilePictureByAccountIdServiceOutput Factory(byte[] profilePicture, string fileType)
        => new GetProfilePictureByAccountIdServiceOutput(profilePicture, fileType);
}
