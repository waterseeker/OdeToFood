using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public ContentResult Index()
        {
            return Content ("Hello, from the HomeController!");
        }
    }
}
