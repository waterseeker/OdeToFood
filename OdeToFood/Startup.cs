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
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
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

            app.UseWelcomePage("/welcome"); //sets the middleware to only respond to the /welcome http adddress
            //you could also set it up like.....
            //app.UseWelcomePage(new WelcomePageOptions
            //{
            //    Path = "/welcome"
            //});
            //and this would do the same thing, only respond to the /welcome path. 

            app.Run(async (context) =>
            {
                //var message = Configuration["Greeting"]; 
                var message = greeter.GetGreeting();
                await context.Response.WriteAsync(message);

                //await context.Response.WriteAsync("Hello world!!!!!!!!");
            });
        }
    }
}
