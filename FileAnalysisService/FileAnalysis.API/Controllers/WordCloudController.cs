using FileAnalysis.Application.UseCases.GenerateWordCloud;
using Microsoft.AspNetCore.Mvc;

namespace FileAnalysis.API.Controllers
{

    [ApiController]
    [Route("api/wordcloud")]
    public class WordCloudController(GenerateWordCloudUseCase useCase)
        : ControllerBase
    {
        private readonly GenerateWordCloudUseCase _useCase = useCase;

        [HttpGet("{fileId:guid}")]
        public ActionResult<GenerateWordCloudResponse> Generate([FromRoute] Guid fileId)
        {
            var response = _useCase.Execute(new GenerateWordCloudRequest(fileId));

            return Ok(response);
        }
    }
}
