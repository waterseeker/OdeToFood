using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OdeToFood
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); //adds default mvc services
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>(); //AddSingleton tells the framework that there will only be one instance of Greeter
            //throughout the app
            services.AddScoped<IRestaurantData, SqlRestaurantData>(); //AddScoped tells the framework there should be one
            //instance of the service for each http request. 
            services.AddDbContext<OdeToFoodDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("OdeToFood")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<OdeToFoodDbContext>();
        }

        // This method gets called by the runtime. 
        //Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // middleware that will display a ui with troubleshootin info if there is an unhandled exception
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Ooooops! Something's gone sideways.")
                    //if the app is ran in a non-dev environment, this will return a more user-friendly error message
                });
 
            }

            app.UseFileServer(); //this is essentially a combination of UseDefaultFiles and UseStaticFiles

            //app.UseDefaultFiles(); //middleware that looks at incoming requests and sees if there's a default file that matches that request. index.html is one 
            //of the default filenames out of the box so this will serve index.html to any root request 
            //app.UseStaticFiles(); //middleware to serve static files. by default it looks for files in the wwwroot folder

            app.UseNodeModules(env.ContentRootPath);

            app.UseIdentity();

            app.UseMvc(ConfigureRoutes);
            //when the mvc inspects the request and sees that it doesn't match any of the routes we've configured, it'll 
            //go to the next middleware.
            //middlewhare where given an http context
            app.Run(ctx => ctx.Response.WriteAsync("Not found"));
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //first param is a name, 2nd is a schema for the route
            // /controllerName/Action
            // /Home/Index
            // a ? means the param is optional
            // the = sets up a default value for the route. 
            //this route config will make any request to the root use the Home controller, 
            //and root/Home, and any root/Home/Index use the Home controller as well. 
            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
