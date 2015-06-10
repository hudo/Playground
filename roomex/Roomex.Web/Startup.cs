using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Roomex.Web.Startup))]

namespace Roomex.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use<ConsoleLoggingMiddleware>();

            // static files middleware stops owin pipeline, so logging middleware will not log requests which are handled by it
            app.UseStaticFiles();

            var configuration = new HttpConfiguration();
            configuration.Formatters.Remove(configuration.Formatters.XmlFormatter);
            configuration.Routes.MapHttpRoute("default", "api/{controller}/{id}", defaults: new { id = RouteParameter.Optional });
            app.UseWebApi(configuration);
        }
    }
}
