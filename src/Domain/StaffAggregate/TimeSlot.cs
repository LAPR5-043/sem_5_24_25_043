using NuGet.Packaging.Signing;
using src.Domain.Shared;
using Microsoft.EntityFrameworkCore;
[Owned]
public class TimeSlot : IValueObject
{
    public Timestamp startDate { get; }
    public Timestamp endDate { get; }
    public bool occupied { get; private set; }

    public TimeSlot(Timestamp startDate, Timestamp endDate, bool occupied){
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
        return startDate + " - " + endDate + " " + (occupied ? "Occupied" : "Free");
    }

}
