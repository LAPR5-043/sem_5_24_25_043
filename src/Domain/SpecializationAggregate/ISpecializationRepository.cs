using src.Domain.Shared;

namespace Domain.SpecializationAggregate
{
    public interface ISpecializationRepository : IRepository<Specialization, SpecializationName>
    {
        Specialization UpdateAsync(Specialization specialization);
    }
}