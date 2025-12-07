using FileStorage.Application.UseCases.GetAllFilesForAnalysis;
using FileStorage.Application.UseCases.GetFileForAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.API.Controllers
{
    [Route("internal/files")]
    [ApiController]
    public class InternalFilesController(GetFileUseCase getFileUseCase, GetByWorkIdUseCase getByWorkIdUseCase) : ControllerBase
    {
        private readonly GetFileUseCase _getFileUseCase = getFileUseCase;
        private readonly GetByWorkIdUseCase _getByWorkIdUseCase = getByWorkIdUseCase;

        [HttpGet("by-work/{workId:guid}")]
        public IActionResult GetByWorkId([FromRoute(Name = "workId")] GetByWorkIdRequest request)
        {
            var result = _getByWorkIdUseCase.Execute(request);
            return Ok(result);
        }

        [HttpGet("{fileId:guid}")]
        public IActionResult GetById([FromRoute(Name = "fileId")] GetFileRequest request)
        {
            var result = _getFileUseCase.Execute(request);
            return Ok(result);
        }
    }
}
