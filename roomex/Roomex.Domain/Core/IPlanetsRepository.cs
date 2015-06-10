using System.Collections.Generic;
using System.Threading.Tasks;
using Roomex.Domain.Model;

namespace Roomex.Domain.Core
{
    /// <summary>
    /// In general, repository abstraction can be considered as an anti-pattern, unless if it really adds some persistency-specific value,
    /// but in this case, where we have really simple CRUD application, it works just fine.
    /// 
    /// Issues with this design:  app can require many types of db queries, which can lead into bloating of IRepository interface with lot
    /// of GetThis, GetThat methods. Also, people put Insert, Save and other write-specific methods, and they belong into Unit of Work specialized
    /// class.
    /// Also, application and service layers usually required a projection of domain model, especially for UI, not the domain model which should be 
    /// contained in Core/domain part of an application
    /// 
    /// In real app, I highly recommend usage of CQS pattern. It solves problems of encapsulation of query parameters, projection as a return type, 
    /// single responsibility principle (SRP) and it scales very well as project grows. 
    /// </summary>
    public interface IPlanetsRepository
    {
        Task<IEnumerable<Planet>> GetAll();
        Task<Option<Planet>> GetById(int id);
    }
}
