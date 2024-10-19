using Domain.StaffAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


public class StaffRepository : BaseRepository<Staff, StaffID>, IStaffRepository
{
    private readonly AppContext context;

    public StaffRepository(AppContext context) : base(context.Staffs)
    {
        this.context = context;
    }

    public void UpdateAsync(Staff staff)
    {
        context.Entry(staff).State = EntityState.Modified;
    }
}

