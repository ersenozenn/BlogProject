using Microsoft.AspNetCore.Mvc;

namespace BlogProject.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
