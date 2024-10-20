using Domain.StaffAggregate;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


public class PendingRequestRepository : BaseRepository<PendingRequest, LongId>, IPendingRequestRepository
{
    private readonly AppContext context;

    public PendingRequestRepository(AppContext context) : base(context.PendingRequests)
    {
        this.context = context;
    }

}