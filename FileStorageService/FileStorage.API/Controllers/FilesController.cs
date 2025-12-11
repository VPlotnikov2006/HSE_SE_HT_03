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
        public ActionResult<SaveFileResponse> Upload([FromBody] SaveFileRequest request)
        {
            var result = _saveFileUseCase.Execute(request);
            return Ok(result);
        }
    }
}
