using Roomex.Domain.Core;

namespace Roomex.Domain.Model
{
    public class Planet : IAggregate
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        /// <summary>
        /// Distance from Sun in km
        /// </summary>
        public long DistanceFromSun { get; set; }

        /// <summary>
        /// Flags the planet as active.
        /// Like Pluto, planet can be "downgraded", and lets assumed they can't be just destroyed by some Deathstar.
        /// This is to support soft-delete.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
