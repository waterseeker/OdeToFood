using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new Restaurant { Id = 1, Name = "The House of Kobe" }; //instantiate a restaurant with an id 1 and name The House of Kobe

            return View();

            //return new ObjectResult(model); //without setup params, this will return a serialized object in json. 

            //return Content ("Hello, from the HomeController!");
        }
    }
}
