using src.Domain.Shared;

namespace Domain.StaffAggregate
{
    public interface IStaffRepository : IRepository<Staff, StaffID>
    {
        void UpdateAsync(Staff staff);
    }
}