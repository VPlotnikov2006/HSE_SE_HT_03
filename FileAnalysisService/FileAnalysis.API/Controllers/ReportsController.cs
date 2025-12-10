using FileAnalysis.Application.UseCases.GetReportsUseCase;
using Microsoft.AspNetCore.Mvc;

namespace FileAnalysis.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController(GetReportsUseCase getReports) : ControllerBase
    {
        private readonly GetReportsUseCase _getReports = getReports;

        [HttpGet("by-work/{workId:guid}")]
        public ActionResult<IReadOnlyCollection<GetReportsResponse>> GetByWorkId([FromRoute] Guid workId)
        {
            var request = new GetReportsRequest(workId);
            var reports = _getReports.Execute(request);

            return Ok(reports);
        }
    }
}
