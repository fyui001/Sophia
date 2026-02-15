namespace Sophia.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    [HttpGet("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Index()
    {
        return Content("高田憂希しか好きじゃない");
    }
}
