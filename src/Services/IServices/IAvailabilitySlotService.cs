using src.Domain.AvailabilitySlotAggregate;

public interface IAvailabilitySlotService
{
    public Task CreateAvailabilitySlotAsync(StaffID availabilitySlotID, List<string> slots);
    public void UpdateAvailabilitySlot(AvailabilitySlot availabilitySlot);
    public Task DeleteAvailabilitySlotAsync(string availabilitySlotID);
    Task<AvailabilitySlot> GetAvailabilitySlotAsync(string availabilitySlotID);
    Task<List<AvailabilitySlot>> GetAllAvailabilitySlotsAsync();
}