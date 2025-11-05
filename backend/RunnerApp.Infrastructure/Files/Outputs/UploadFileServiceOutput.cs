namespace RunnerApp.Infrastructure.Files.Outputs;

public readonly struct UploadFileServiceOutput
{
    public string FileName { get; }
    public string FileUrl { get; }
    public double FileSize { get; }

    private UploadFileServiceOutput(string fileName, string fileUrl, double fileSize)
    {
        FileName = fileName;
        FileUrl = fileUrl;
        FileSize = fileSize;
    }

    public static UploadFileServiceOutput Factory(string fileName, string fileUrl, double fileSize)
        => new UploadFileServiceOutput(fileName, fileUrl, fileSize);
}
