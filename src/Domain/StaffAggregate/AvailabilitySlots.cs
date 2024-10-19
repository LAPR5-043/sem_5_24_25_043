using src.Domain.Shared;

public class AvailabilitySlots : IValueObject   {
    public List<TimeSlot> slots { get; }

    public AvailabilitySlots(List<TimeSlot> slots) {
        this.slots = slots;
    }

    public AvailabilitySlots() {
        this.slots = new List<TimeSlot>();
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        AvailabilitySlots availabilitySlots = (AvailabilitySlots)obj;
        return slots.SequenceEqual(availabilitySlots.slots);
    }

    public override int GetHashCode() {
        return slots.GetHashCode();
    }


}