using src.Domain.Shared;

public class EstimatedDuration : IValueObject
{
    public int hours { get; }
    public int minutes { get; }

    public EstimatedDuration(int hours, int minutes)
    {
        this.hours = hours;
        this.minutes = minutes;
    }
    public EstimatedDuration()
    {

    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        EstimatedDuration estimatedDuration = (EstimatedDuration)obj;
        return hours == estimatedDuration.hours && minutes == estimatedDuration.minutes;
    }

    public override int GetHashCode() {
        return hours.GetHashCode() ^ minutes.GetHashCode();
    }

    public override string ToString()
    {
        return hours + ":" + minutes ;
    }

}
