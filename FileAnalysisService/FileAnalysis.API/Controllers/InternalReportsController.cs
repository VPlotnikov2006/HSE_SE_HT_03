using FileAnalysis.Application.UseCases.AnalyseFileUseCase;
using Microsoft.AspNetCore.Mvc;

namespace FileAnalysis.API.Controllers
{
    [ApiController]
    [Route("internal/analysis")]
    public class InternalAnalysisController(AnalyseFileUseCase analyseFile) : ControllerBase
    {
        private readonly AnalyseFileUseCase _analyseFile = analyseFile;

        [HttpPost]
        public ActionResult<AnalyseFileResponse> Analyse([FromBody] AnalyseFileRequest request)
        {
            var result = _analyseFile.Execute(request);
            return Ok(result);
        }
    }
}
