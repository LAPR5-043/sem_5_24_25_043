
using Domain.StaffAggregate;
using Microsoft.EntityFrameworkCore;
using src.Domain.AvailabilitySlotAggregate;
using src.Infrastructure.Shared;
using AppContext = src.Models.AppContext;

public class AvailabilitySlotRepository : BaseRepository<AvailabilitySlot, StaffID>, IAvailabilitySlotRepository
{
    private readonly AppContext context;

    public AvailabilitySlotRepository(AppContext context) : base(context.AvailabilitySlots)
    {
        this.context = context;
    }


    public void UpdateAsync(AvailabilitySlot AvailabilitySlot)
    {
        context.Entry(AvailabilitySlot).State = EntityState.Modified;
    }
}
