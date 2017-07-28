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
        void Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            return newRestaurant;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }
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

        public void Commit()
        { }
            //since the in memory is not transactional, this is really a no op or no operation. if you set a prop
            //on an inmemory data source, that change is already committed. 

            static List<Restaurant> _restaurants; //static means there will only be once instance on this list for the entire app and we'll
                                                  //always be looking at the same list of restaurants. 

        
    }
}
