using Microsoft.AspNetCore.Mvc;


namespace OdeToFood.Controllers
{
    [Route("[controller]/[action]")] //grabs name of the controller, so this will serve for the About controller, and then the name 
    //of the action. The action names are defined by the methods below. 
    
    
    //[Route("about")] //first part of http request is /about/
    public class AboutController
    {
        //[Route("[action]")] //uses the method name to match the action part of the http request. this will 
        //serve for the /phone action on the http request. 
        //using the [action] token allows the name of the Phone method to change without having to change the routing. It'll automatically
        //pick up the name of the method. 
        
        //[Route("")] //action in http request is blank
        public string Phone() //responds to the phone action on the http request of the about page... /about/phone
        {
            return "1-555-555-5555";
        }

        //[Route("[action]")] // /about/address
        
        //[Route("address")] //about/address
        public string Address() 
        {
            return "USA";
        }
    }
}
