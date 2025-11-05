namespace RunnerApp.Infrastructure.Files.Interfaces;

using Microsoft.AspNetCore.Http;
using RunnerApp.Infrastructure.Files.Outputs;

public interface IFileService
{
    public Task<UploadFileServiceOutput> UploadFileServiceAsync(
        IFormFile file,
        CancellationToken cancellationToken);

    public void DeleteFile(
        string filePath,
        CancellationToken cancellationToken);
}
