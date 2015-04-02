using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            var server = WebApp.Start("http://+:56789", builder =>
            {
                var configuration = new HttpConfiguration();
                configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
                configuration.Routes.MapHttpRoute("default", "api/{controller}");
                configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
                configuration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                builder.UseWebApi(configuration);
            });

            var httpClient = new HttpClient();

            var payload = httpClient.GetStringAsync("http://localhost:56789/api/test").Result;

            var sampleResponse = JsonConvert.DeserializeObject<SampleResponse>(payload, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            server.Dispose();
            Console.WriteLine("done.");
        }
    }

    public class TestController : ApiController
    {
        public SampleResponse Get()
        {
            return new SampleResponse { Name = "Banana", Vehicle = new Car { Brand = "Rimac" }};
        }
    }

    public class SampleResponse
    {
        public string Name { get; set; }
        public Vehicle Vehicle { get; set; }
    }

    
    public abstract class Vehicle { }

    public class Car : Vehicle
    {
        public string Brand { get; set; }
    }
}
