using Microsoft.AspNetCore.Mvc;

namespace Task.web.Areas.Demos.Controllers
{
    public class MyFirstDemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
