using Gateway.API.DTOs;
using Gateway.Application.DTOs.FileAnalysisDTOs.GetReports;
using Gateway.Application.DTOs.FileStorageDTOs;
using Gateway.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController(
        SaveFileUseCase saveFileUseCase,
        GetReportsUseCase getReportsUseCase) : ControllerBase
    {
        private readonly SaveFileUseCase _saveFileUseCase = saveFileUseCase;
        private readonly GetReportsUseCase _getReportsUseCase = getReportsUseCase;

        [HttpPost("files")]
        [Consumes("multipart/form-data")]
        public ActionResult<SaveFileResponse> SaveFile([FromForm] SaveFileRequestApi request)
        {
            if (request.Content == null || request.Content.Length == 0)
                return BadRequest("File content is empty");

            byte[] bytes;

            using (var ms = new MemoryStream())
            {
                request.Content.CopyTo(ms);
                bytes = ms.ToArray();
            }

            var internalRequest = new SaveFileRequest(
                request.OriginalName,
                bytes,
                request.Owner,
                request.WorkId
            );

            var result = _saveFileUseCase.Execute(internalRequest);
            return Ok(result);
        }

        [HttpGet("reports/{workId:guid}")]
        public ActionResult<GetReportsResponse> GetReports(Guid workId)
        {
            var req = new GetReportsRequest(workId);
            var response = _getReportsUseCase.Execute(req);
            return Ok(response);
        }
    }
}
