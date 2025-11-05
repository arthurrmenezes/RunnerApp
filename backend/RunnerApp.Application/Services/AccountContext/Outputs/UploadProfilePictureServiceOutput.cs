namespace RunnerApp.Application.Services.AccountContext.Outputs;

public readonly struct UploadProfilePictureServiceOutput
{
    public string FileName { get; }
    public string FileUrl { get; }
    public double FileSize { get; }

    private UploadProfilePictureServiceOutput(string fileName, string fileUrl, double fileSize)
    {
        FileName = fileName;
        FileUrl = fileUrl;
        FileSize = fileSize;
    }

    public static UploadProfilePictureServiceOutput Factory(string fileName, string fileUrl, double fileSize)
        => new UploadProfilePictureServiceOutput(fileName, fileUrl, fileSize);
}
