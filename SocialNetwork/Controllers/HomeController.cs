using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    //[Route("")]
    //[Route("[controller]/[action]")] §ï§ä§à §Ý§Ú§ê§ß§Ö§Ö, §ä§Ñ§Ü §Ü§Ñ§Ü §Ú §Ò§Ö§Ù §ä§à§Ô§à §Õ§à§ã§ä§å§á§ß§à §á§à §å§Þ§à§Ý§é§Ñ§ß§Ú§ð
    public IActionResult Index()
    {
        return View(new MainViewModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //Duration = 0 - §ß§Ö §Ü§ï§ê§Ú§â§à§Ó§Ñ§ä§î, §ß§Ú§Ü§Ñ§Ü§à§Ô§à §Ü§ï§ê§Ñ
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //RequestId - §å§ß§Ú§Ü§Ñ§Ý§î§ß§í§Û id, §ã§à§Ù§Õ§Ñ§Ö§ä§ã§ñ §Ý§Ú§Ò§à §é§Ö§â§Ö§Ù §ä§â§Ñ§ã§ã§Ú§â§à§Ó§Ü§å (Activity), §Ý§Ú§Ò§à ID §Ù§Ñ§á§â§à§ã§Ñ, §Ü§à§ä§à§â§í§Û §ã§à§Ù§Õ§Ñ§×§ä §ã§Ñ§Þ ASP.NET Core (HttpContext.TraceIdentifier).
    }
}