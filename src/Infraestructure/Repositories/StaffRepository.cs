using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


public class StaffRepository : BaseRepository<Staff, StaffID>
{
    private readonly AppContext context;

    public StaffRepository(AppContext context) : base(context.Staff)
    {
        this.context = context;
    }
}

