using SimpleThrottler.Engine;

namespace SimpleThrottler.Storage
{
    public class InMemoryRequestStore : IRequestStore
    {
        public RequestEvent Find(string IPAddress)
        {
            return default(RequestEvent);
        }

        public void Add(RequestEvent requestEvent)
        {
            
        }

        public void Remove(RequestEvent requestEvent)
        {
            
        }
    }
}