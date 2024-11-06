using src.Domain.AvailabilitySlotAggregate;
using src.Domain.Shared;

namespace Domain.StaffAggregate
{
    public interface IAvailabilitySlotRepository : IRepository<AvailabilitySlot, StaffID>
    {
        void UpdateAsync(AvailabilitySlot AvailabilitySlot);
       
    }
}