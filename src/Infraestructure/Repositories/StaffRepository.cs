using Domain.StaffAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;

/// <summary>
/// Staff repository
/// </summary>
public class StaffRepository : BaseRepository<Staff, StaffID>, IStaffRepository
{
    /// <summary>
    /// App context
    /// </summary>
    private readonly AppContext context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public StaffRepository(AppContext context) : base(context.Staffs)
    {
        this.context = context;
    }

    /// <summary>
    /// Updated staff member's information
    /// </summary>
    /// <param name="staff"></param>
    /// <returns></returns>
    public void UpdateAsync(Staff staff)
    {
        context.Entry(staff).State = EntityState.Modified;
    }

    /// <summary>
    /// Check if staff member already exists
    /// </summary>
    /// <param name="email"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public bool StaffExists(string email, string phoneNumber)
    {
        return context.Staffs
            .AsEnumerable()
            .Any(p => p.email.email == email || p.phoneNumber.phoneNumber == phoneNumber);
    }

    /// <summary>
    /// Retrieves staff member by email
    /// </summary>
    /// <param name="doctorEmail"></param>
    /// <returns>Staff Member</returns>
    public async Task<Staff> GetStaffByEmail(string doctorEmail)
    {
        return context.Staffs
            .AsEnumerable()
            .FirstOrDefault(s => s.email.email == doctorEmail)!;
    }
}

