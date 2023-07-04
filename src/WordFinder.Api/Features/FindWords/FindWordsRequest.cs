using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WordFinder.Api.Features.FindWords;

public record FindWordsRequest(string Letters) : IRequest<FindWordsResponse>;
