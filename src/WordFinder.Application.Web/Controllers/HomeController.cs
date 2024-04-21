using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Diagnostics;
using System.Text;
using WordFinder.Web.Models;
using WordFinder.Web.Services;

namespace WordFinder.Web.Controllers;

public sealed class HomeController : Controller
{
    private readonly IWordsService _wordsService;

    public HomeController(IWordsService wordsService) => _wordsService = wordsService;

    public IActionResult Index(string letters, int minLen, string contains, string startsWith, string endsWith)
    {
        if (string.IsNullOrWhiteSpace(letters))
        {
            return View();
        }

        var model = _wordsService.FindWords(new SearchOptions(letters, minLen, contains, startsWith, endsWith));
        ViewData["ResultMsg"] = GetResultMessage(letters, minLen, contains, startsWith, endsWith);
        return View(model);
    }

    private static string GetResultMessage(string letters, int minLen, string contains, string startsWith, string endsWith)
    {
        var msg = new StringBuilder("Words with '");
        msg.Append(letters);
        msg.Append('\'');
        if (!string.IsNullOrWhiteSpace(contains)) msg.Append($" containing '{contains}'");
        if (!string.IsNullOrWhiteSpace(startsWith)) msg.Append($", starting with '{startsWith}'");
        if (!string.IsNullOrWhiteSpace(endsWith)) msg.Append($", ending with '{endsWith}'");
        msg.Append($" and having min length {minLen} chars");
        return msg.ToString();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    public IActionResult About() => View();
}