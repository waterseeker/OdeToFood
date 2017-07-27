using OdeToFood.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OdeToFood.Services
{

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant newRestaurant);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "The House of Kobe"},
                new Restaurant { Id = 2, Name = "LJ's and the Kat"},
                new Restaurant { Id = 3, Name = "The King's Contrivance"}
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1; //look at the restaurant list length and add one to it and assign that value
            //to the id prop of the new restaurant. 
            _restaurants.Add(newRestaurant); //add the new restaurant to the list. 
            return newRestaurant;
        }

        static List<Restaurant> _restaurants; //static means there will only be once instance on this list for the entire app and we'll
        //always be looking at the same list of restaurants. 

    }
}
