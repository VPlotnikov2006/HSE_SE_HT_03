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
        public IActionResult Generate([FromRoute] Guid fileId)
        {
            var response = _useCase.Execute(new GenerateWordCloudRequest(fileId));

            return File(response.ImageBytes, "image/png");
        }
    }
}
