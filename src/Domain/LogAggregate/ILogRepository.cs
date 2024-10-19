using src.Domain.Shared;

namespace Domain.LogAggregate
{
    public interface ILogRepository : IRepository<Log, LongId>
    {

    }
}