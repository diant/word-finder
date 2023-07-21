using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Web.Models;
using WordFinder.Web.Services;

namespace WordFinder.Web.Controllers;

public sealed class HomeController : Controller
{
    private readonly IWordsService _wordsService;

    public HomeController(IWordsService wordsService)
    {
        _wordsService = wordsService;
    }

    public async Task<IActionResult> Index(string letters)
    {
        if (string.IsNullOrWhiteSpace(letters))
        {
            return View();
        }

        var model = await _wordsService.FindWordsAsync(new SearchOptions(letters));
        ViewData["Letters"] = letters;
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}