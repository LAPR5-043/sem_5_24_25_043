using NuGet.Packaging.Signing;
using src.Domain.Shared;
using System;
public class TimeSlot : IValueObject
{
    public DateTime startDate { get; }
    public DateTime endDate { get; }
    public bool occupied { get; private set; }

    public TimeSlot(DateTime startDate, DateTime endDate, bool occupied){
        this.startDate = startDate;
        this.endDate = endDate;
        this.occupied = occupied;
    }

    public TimeSlot(){

    }

    public void MarkAsOccupied(){
        this.occupied = true;
    }

    public void MarkAsFree(){
        this.occupied = false;
    }

    public override bool Equals(object obj){
        if(obj == null || GetType() != obj.GetType()){
            return false;
        }
        TimeSlot timeSlot = (TimeSlot)obj;
        return startDate == timeSlot.startDate && endDate == timeSlot.endDate && occupied == timeSlot.occupied;
    }

    public override int GetHashCode() {
        return startDate.GetHashCode() + endDate.GetHashCode() + occupied.GetHashCode();
    }

    public override string ToString()
    {
        return startDate.ToString() + "," + endDate.ToString() + "," + (occupied ? "true" : "false");
    }

    public static List<TimeSlot> timeSlotsFromString(List<string> slots)
{
    List<TimeSlot> timeSlots = new List<TimeSlot>();
    foreach (var slot in slots)
    {
        var parts = slot.Split(',');
        if (parts.Length == 3)
        {
            DateTime startDate = DateTime.Parse(parts[0]);
            DateTime endDate = DateTime.Parse(parts[1]);
            bool occupied = bool.Parse(parts[2]);

            timeSlots.Add(new TimeSlot(startDate, endDate, occupied));
        }
    }
    return timeSlots;
}

}
