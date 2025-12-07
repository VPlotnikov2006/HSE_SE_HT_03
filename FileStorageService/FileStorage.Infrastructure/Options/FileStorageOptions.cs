namespace FileStorage.Infrastructure.Options;

public record FileStorageOptions()
{
    public required string RootPath { get; init; }
}
