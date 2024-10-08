public class TimeSlots
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public bool Occupied { get; }
    
    public TimeSlots(DateTime startDate, DateTime endDate, bool occupied)
    {
        StartDate = startDate;
        EndDate = endDate;
        Occupied = occupied;
    }
}