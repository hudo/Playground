using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Roomex.Domain.Core;
using Roomex.Domain.Model;

namespace Roomex.Persistency.Inmemory
{
    /// <summary>
    /// In memory implementation of Planets Repository.
    /// For design decision, check comments on interface <see cref="IPlanetsRepository"/>
    /// </summary>
    public sealed class PlanetsRepository : IPlanetsRepository
    {
        private static readonly IEnumerable<Planet> Planets;
 
        static PlanetsRepository()
        {
            Planets = new[]
            {
                new Planet {Id = 1, Name = "Mercury", DistanceFromSun = 57910000},
                new Planet {Id = 2, Name = "Venus", DistanceFromSun = 108200000},
                new Planet {Id = 3, Name = "Earth", DistanceFromSun = 149600000},
                new Planet {Id = 4, Name = "Mars", DistanceFromSun = 227940000},
                new Planet {Id = 5, Name = "Jupiter", DistanceFromSun = 778330000},
                new Planet {Id = 6, Name = "Saturn", DistanceFromSun = 1424600000},
                new Planet {Id = 7, Name = "Uranus", DistanceFromSun = 4501000000},
                new Planet {Id = 8, Name = "Neptune", DistanceFromSun = 5945900000}
            };
        }


        public Task<IEnumerable<Planet>> GetAll()
        {
            // we want to create new collection with copies elements, so caller can't change our internal in-memory structure
            return Task.FromResult(Planets.ToArray().AsEnumerable());
        }

        public Task<Option<Planet>> GetById(int id)
        {
            var planet = Planets.FirstOrDefault(p => p.Id == id);

            return Task.FromResult(planet == null ? Option<Planet>.None() : Option<Planet>.Some(planet));
        }
    }
}