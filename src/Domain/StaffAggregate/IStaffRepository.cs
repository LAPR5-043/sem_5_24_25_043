using src.Domain.Shared;

namespace Domain.StaffAggregate
{
    public interface IStaffRepository : IRepository<Staff, StaffID>
    {
        void UpdateAsync(Staff staff);
        Boolean StaffExists(string email, string phoneNumber, string licenseNumber);
        Task<Staff> GetStaffByEmail(string doctorEmail);
        Task<Staff> GetStaffByID(string staffId);
    }
}