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
    //[Route("[controller]/[action]")] ���� �ݧڧ�ߧ֧�, ��ѧ� �ܧѧ� �� �ҧ֧� ���ԧ� �է�����ߧ� ��� ��ާ�ݧ�ѧߧڧ�
    public IActionResult Index()
    {
        return View(new MainViewModel());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] //Duration = 0 - �ߧ� �ܧ��ڧ��ӧѧ��, �ߧڧܧѧܧ�ԧ� �ܧ���
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //RequestId - ��ߧڧܧѧݧ�ߧ�� id, ���٧էѧ֧��� �ݧڧҧ� ��֧�֧� ���ѧ��ڧ��ӧܧ� (Activity), �ݧڧҧ� ID �٧ѧ�����, �ܧ������ ���٧էѧק� ��ѧ� ASP.NET Core (HttpContext.TraceIdentifier).
    }
}