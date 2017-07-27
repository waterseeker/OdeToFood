using Microsoft.AspNetCore.Mvc;


namespace OdeToFood.Controllers
{

    [Route("about")] //first part of http request is /about/
    public class AboutController
    {
        [Route("")] //action in http request is blank
        public string Phone() //responds to the phone action on the http request of the about page... /about/phone
        {
            return "1-555-555-5555";
        }

        public string Address() // /about/address
        {
            return "USA";
        }
    }
}
