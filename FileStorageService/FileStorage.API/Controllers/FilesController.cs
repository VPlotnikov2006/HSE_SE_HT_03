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
        public IActionResult Upload([FromForm] SaveFileRequest request)
        {
            var result = _saveFileUseCase.Execute(request);
            return Ok(result);
        }
    }
}
