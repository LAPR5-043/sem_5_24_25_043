using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;


public class StaffRepository : BaseRepository<Staff, StaffID>
{
    public StaffRepository(DbSet<Staff> objs) : base(objs)
    {
    }
}