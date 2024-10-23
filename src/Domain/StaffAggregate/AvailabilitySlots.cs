using src.Domain.Shared;

/// <summary>
/// Value object that represents the availability slots of a staff member.
/// </summary>
public class AvailabilitySlots : IValueObject
{
    /// <summary>
    /// List of time slots.
    /// </summary>
    public List<TimeSlot> slots { get; }

    /// <summary>
    /// Constructor that initializes the list of time slots
    /// </summary>
    /// <param name="slots"></param>
    public AvailabilitySlots(List<TimeSlot> slots)
    {
        this.slots = slots;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public AvailabilitySlots()
    {
        this.slots = new List<TimeSlot>();
    }

    /// <summary>
    /// Compares two instances of Availability Slots.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        AvailabilitySlots availabilitySlots = (AvailabilitySlots)obj;
        return slots.SequenceEqual(availabilitySlots.slots);
    }

    /// <summary>
    /// Returns the hash code of the Availability Slots.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return slots.GetHashCode();
    }


}