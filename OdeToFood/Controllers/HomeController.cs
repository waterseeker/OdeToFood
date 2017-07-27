using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;
using OdeToFood.ViewModels;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter) //give me an instance of the IRestaurantData called restaurantData
             //and an instance of the IGreeter called greeter
        {
            _restaurantData = restaurantData; //saves the restaurantdata so we can consume it elsewhere
            _greeter = greeter; //saves the greeter so we can use it elsewhere
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel(); //sets the model to an instance of the HomePageViewModel
            model.Restaurants = _restaurantData.GetAll(); 
            model.CurrentMessage = _greeter.GetGreeting();
            //var model = _restaurantData.GetAll();
            //var model = new Restaurant { Id = 1, Name = "The House of Kobe" }; //instantiate a restaurant with an id 1 and name The House of Kobe

            return View(model);

            //return new ObjectResult(model); //without setup params, this will return a serialized object in json. 

            //return Content ("Hello, from the HomeController!");
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null)
            {
                return RedirectToAction("Index"); //if there's an invalid http request redirect the user back to the index page. 
            }
            return View(model);
        }

        public IActionResult Create () //this signature of the Create returns a View with a form 
        {
            return View();
        }

        public IActionResult Create () //this signature is where the form posts to
        {

        }
    }
}
