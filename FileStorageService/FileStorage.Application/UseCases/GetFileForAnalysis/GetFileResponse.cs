using FileStorage.Domain;

namespace FileStorage.Application.UseCases.GetFileForAnalysis;

public class GetFileResponse
{
    public FileMetadata Metadata { get; private set; }
    public byte[] Content { get; private set; }

    public GetFileResponse(
        FileMetadata metadata,
        byte[] content
    )
    {
        if (Utils.Checksum.Compute(content) != metadata.Checksum)
        {
            throw new ArgumentException("Checksum mismatch");
        }
        
        Metadata = metadata;
        Content = content;
    }
}
