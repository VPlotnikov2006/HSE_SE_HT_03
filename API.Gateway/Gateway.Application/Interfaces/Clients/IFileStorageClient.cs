using Gateway.Application.DTOs.FileStorageDTOs;

namespace Gateway.Application.Interfaces.Clients;

public interface IFileStorageClient
{
    SaveFileResponse SaveFile(SaveFileRequest request);
}
