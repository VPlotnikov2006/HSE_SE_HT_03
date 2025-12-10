using FileAnalysis.Application.DTOs;

namespace FileAnalysis.Application.Interfaces;

public interface IFileStorageClient
{
    IReadOnlyCollection<Guid> GetFilesIdByWorkId(Guid workId);
    FileContentDto GetFileContent(Guid fileId);
}
