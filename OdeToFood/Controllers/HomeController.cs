using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData) //give me an instance of the IRestaurantData called restaurantData
        {
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            var model = _restaurantData.GetAll();
            //var model = new Restaurant { Id = 1, Name = "The House of Kobe" }; //instantiate a restaurant with an id 1 and name The House of Kobe

            return View(model);

            //return new ObjectResult(model); //without setup params, this will return a serialized object in json. 

            //return Content ("Hello, from the HomeController!");
        }
    }
}
