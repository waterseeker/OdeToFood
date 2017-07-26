using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood
{

    public interface IGreeter //describes the capabilities of every greeter
    {
        string GetGreeting(); //all can run the GetGreeting method
    }


    public class Greeter : IGreeter //class to implement the interface
    {
        private string _greeting;

        public Greeter(IConfiguration configuration) //constructor for this class
        {
            _greeting = configuration["Greeting"]; //reads greeting value from appsettings.json
        }
        public string GetGreeting()
        {
            //return "Hola from the Greeter!"; //sets the return value of the GetGreeting method for this implementation of IGreeter
            return _greeting; //returns value read from the IConfiguration source
        }
    }
}
