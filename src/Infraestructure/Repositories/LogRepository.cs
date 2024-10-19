using Domain.LogAggregate;
using Domain.StaffAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


public class LogRepository : BaseRepository<Log, LongId>, ILogRepository
{
    private readonly AppContext context;

    public LogRepository(AppContext context) : base(context.Logs)
    {
        this.context = context;
    }

}
