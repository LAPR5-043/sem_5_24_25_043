using Domain.StaffAggregate;
using src.Domain.AvailabilitySlotAggregate;
using src.Domain.Shared;

public class AvailabilitySlotService : IAvailabilitySlotService
{
    private readonly IAvailabilitySlotRepository _availabilitySlotRepository;
    private  readonly IUnitOfWork unitOfWork;

    public AvailabilitySlotService(IAvailabilitySlotRepository availabilitySlotRepository, IUnitOfWork unitOfWork)
    {
        _availabilitySlotRepository = availabilitySlotRepository;
        this.unitOfWork = unitOfWork;
    }
    public void CreateAvailabilitySlot(StaffID availabilitySlotID, List<string> availabilitySlots){
        AvailabilitySlot av = new AvailabilitySlot();
        av.Id = availabilitySlotID;
        av.StaffID = availabilitySlotID;


        foreach (var slot in availabilitySlots)
        {
            string[] slots = slot.Split(",");
            av.addAvailabilitySlot(int.Parse(slots[0]), int.Parse(slots[1]), int.Parse(slots[2]));
        }

        _availabilitySlotRepository.AddAsync(av);
        unitOfWork.CommitAsync();
        
    }
    public void UpdateAvailabilitySlot(AvailabilitySlot availabilitySlot){
        _availabilitySlotRepository.UpdateAsync(availabilitySlot);
        unitOfWork.CommitAsync();
    }
    public async Task DeleteAvailabilitySlotAsync(string availabilitySlotID){
        AvailabilitySlot availabilitySlot = await _availabilitySlotRepository.GetByIdAsync(new StaffID(availabilitySlotID));
        _availabilitySlotRepository.Remove(availabilitySlot);
        unitOfWork.CommitAsync();
    }
    public async Task<AvailabilitySlot> GetAvailabilitySlotAsync(string availabilitySlotID){
        return await _availabilitySlotRepository.GetByIdAsync(new StaffID(availabilitySlotID));
    }


}