using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Core;

namespace WordFinder.Api.Features.Words
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class WordsController : ControllerBase
    {
        private readonly IMediator mediator;

        public WordsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetWordsWithQ(bool withU = true)
        {
            var wordsWithQ = await mediator.Send(new WordsWithQRequest(withU));
            return Ok(wordsWithQ);
        }
    }

    public record WordsWithQRequest(bool WithU = true) : IRequest<WordsWithQResponse>;
    public record WordsWithQResponse(IReadOnlyCollection<Word> WordsWithQ);

    public sealed class WordsWithQHandler : IRequestHandler<WordsWithQRequest, WordsWithQResponse>
    {
        public async Task<WordsWithQResponse> Handle(WordsWithQRequest request, CancellationToken cancellationToken)
        {
            var words = await Core.WordFinder.FindWithQ(request.WithU);
            return new WordsWithQResponse(words);
        }
    }
}
