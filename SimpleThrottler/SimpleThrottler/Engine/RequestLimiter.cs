using System.Threading.Tasks;

namespace SimpleThrottler.Engine
{
    public class RequestLimiter
    {
        private readonly IRequestStore _requestStore;

        public RequestLimiter(IRequestStore requestStore)
        {
            _requestStore = requestStore;
        }

        public Task<bool> Allowed(string ipAddress)
        {
            
        }
    }
}