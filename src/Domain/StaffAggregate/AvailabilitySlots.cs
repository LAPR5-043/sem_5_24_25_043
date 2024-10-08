public class AvailabilitySlots
{
    public List<TimeSlots> Slots { get; }
    
    public AvailabilitySlots(List<TimeSlots> slots)
    {
        Slots = slots;
    }
}