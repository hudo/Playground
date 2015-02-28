namespace SimpleThrottler.Engine
{
    public interface IRequestStore
    {
        RequestEvent Find(string IPAddress);
        void Add(RequestEvent requestEvent);
        void Remove(RequestEvent requestEvent);
    }
}