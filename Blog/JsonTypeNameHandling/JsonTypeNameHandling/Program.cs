using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Owin;

namespace JsonTypeNameHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = WebApp.Start<Startup>("http://+:56789");

            var httpClient = new HttpClient();

            var payload = httpClient.GetStringAsync("http://localhost:56789/api/test").Result;

            var sampleResponse = JsonConvert.DeserializeObject<SampleResponse>(payload, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            server.Dispose();
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use((ctx, next) =>
            {
                Console.WriteLine("Request: " + ctx.Request.Uri);
                return next();
            });

            var configuration = new HttpConfiguration();
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            configuration.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            appBuilder.UseWebApi(configuration);
        }
    }

    public class TestController : ApiController
    {
        public SampleResponse Get()
        {
            return new SampleResponse { Name = "foo", Vehicle = new Bike { Brand = "Giant" }};
        }
    }

    public class SampleResponse
    {
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    
    public abstract class Vehicle { }

    public class Car : Vehicle { }

    public class Bike : Vehicle
    {
        public string Brand { get; set; }
    }
}
