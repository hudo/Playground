using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Roomex.Domain.Core;
using Roomex.Persistency.Inmemory;

namespace Roomex.Web.Api
{
    public class PlanetsController : ApiController
    {
        private readonly IPlanetsRepository _repository;

        public PlanetsController() : this(new PlanetsRepository()) {  } // poor's man IoC/DI

        public PlanetsController(IPlanetsRepository repository)
        {
            _repository = repository;
        }

        public async Task<IHttpActionResult> Get()
        {
            // in real application, we would have a projection here containing just the data needed for UI, not a real domain model. 
            var planets = await _repository.GetAll();

            // but to simulate projected response, we'll do in-memory transformation
            return Ok(
                from planet in planets
                select new {planet.Id, planet.Name});
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var planet = await _repository.GetById(id);

            if (planet.IsNone) return NotFound();

            return Ok(planet.Value);
        }
    }
}