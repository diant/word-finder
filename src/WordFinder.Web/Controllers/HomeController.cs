using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WordFinder.Web.Models;

namespace WordFinder.Web.Controllers;

public sealed class HomeController : Controller
{
    public async Task<IActionResult> Index(string letters)
    {
        if (string.IsNullOrWhiteSpace(letters))
        {
            return View();
        }

        var words = await Core.WordFinder.Find(letters);

        return View(new WordViewModel(words.Select(x => x.Value).ToList()));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}