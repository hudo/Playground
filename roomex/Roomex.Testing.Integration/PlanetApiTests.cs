using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using Roomex.Domain.Model;
using Roomex.Web;

namespace Roomex.Testing.Integration
{
    [TestFixture]
    public class PlanetApiTests
    {
        private readonly TestServer _server;

        public PlanetApiTests()
        {
            _server = TestServer.Create<Startup>();
        }

        [Test]
        public async Task Get_all_planets()
        {
            var response = await _server.HttpClient.GetAsync("/api/planets");

            var payload = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(string.IsNullOrEmpty(payload));

            var planets = JsonConvert.DeserializeObject<IEnumerable<Planet>>(payload);

            Assert.IsNotNull(planets);
            Assert.AreEqual(8, planets.Count());
            Assert.IsTrue(planets.All(planet => planet.DistanceFromSun == 0));
        }

        [Test]
        public async Task Get_planet_by_id()
        {
            var response = await _server.HttpClient.GetAsync("/api/planets/1");

            var payload = await response.Content.ReadAsStringAsync();

            Assert.IsFalse(string.IsNullOrEmpty(payload));

            var planet = JsonConvert.DeserializeObject<Planet>(payload);

            Assert.IsNotNull(planet);
            Assert.IsNotNullOrEmpty(planet.Name);
            Assert.IsTrue(planet.DistanceFromSun > 0);
        }

        [Test]
        public async Task Get_unknown_planet_returns_http_not_found()
        {
            var response = await _server.HttpClient.GetAsync("/api/planets/99");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
