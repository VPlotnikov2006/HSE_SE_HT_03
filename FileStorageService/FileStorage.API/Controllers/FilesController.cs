using FileStorage.API.DTOs;
using FileStorage.Application.UseCases.SaveFile;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController(
        SaveFileUseCase saveFileUseCase) : ControllerBase
    {
        private readonly SaveFileUseCase _saveFileUseCase = saveFileUseCase;

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] SaveFileRequestApi request)
        {
            using var ms = new MemoryStream();
            await request.Content.CopyToAsync(ms);

            var appRequest = new SaveFileRequest(
                request.OriginalName,
                ms.ToArray(),
                request.Owner,
                request.WorkId
            );

            var result = _saveFileUseCase.Execute(appRequest);

            return Ok(result);
        }
    }
}
