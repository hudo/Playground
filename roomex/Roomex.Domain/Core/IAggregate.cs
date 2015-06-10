namespace Roomex.Domain.Core
{
    public interface IAggregate
    {
        int Id { get; }
        bool IsActive { get; }
    }
}
