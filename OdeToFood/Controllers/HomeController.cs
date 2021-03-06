﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Entities;
using OdeToFood.Services;
using OdeToFood.ViewModels;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace OdeToFood.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter) //give me an instance of the IRestaurantData called restaurantData
             //and an instance of the IGreeter called greeter
        {
            _restaurantData = restaurantData; //saves the restaurantdata so we can consume it elsewhere

        }
        [AllowAnonymous] //makes an exception to the Authorize tag so anonymous users can still see the index of restaurants. 
        public IActionResult Index()
        {
            var model = new HomePageViewModel(); //sets the model to an instance of the HomePageViewModel
            model.Restaurants = _restaurantData.GetAll(); 

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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, RestaurantEditViewModel model)
        {
            var restaurant = _restaurantData.Get(id);
            if (ModelState.IsValid)
            {
                restaurant.Cuisine = model.Cuisine;
                restaurant.Name = model.Name;
                _restaurantData.Commit();
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            return View(restaurant);
        }


        [HttpGet] //route constraint. this tells the browser to use this signature for a get request
        public IActionResult Create () //this signature of the Create returns a View with a form 
        {
            return View();
        }
        [HttpPost] //route constraint that tells the browser to use this signature for a post request. 
        [ValidateAntiForgeryToken] //checks to make sure the form is coming from a form we gave to the user
        //helps prevent cross-site forgeries. 
        public IActionResult Create (RestaurantEditViewModel model) //this signature is where the form posts to
            //we made a separate view model for this to make sure we didn't allow the end user access to any properties we don't
            //want editable on the actual Restaurant entity. 
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Cuisine = model.Cuisine; //the cuisine type is = to the incoming restaurant cuisine
                newRestaurant.Name = model.Name; //the restaurant name is = to the incoming restaurant name
                newRestaurant = _restaurantData.Add(newRestaurant);
                _restaurantData.Commit();

                return RedirectToAction("Details", new { id = newRestaurant.Id }); //second param is a route option, here it is 
                //grabbing the id from the newRestaurante and redirecting the user to the details page of the newRestaurant
                //to avoid double-posting. 
            }
                return View();         
       }
    }
}
