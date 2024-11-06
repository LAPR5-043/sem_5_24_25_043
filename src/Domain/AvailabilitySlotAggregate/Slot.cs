
using src.Domain.Shared;

namespace src.Domain.AvailabilitySlotAggregate{
   
   
    public class Slot : IValueObject
    {
        public int StartTime { get; set; }
        public int EndTime { get; set; }


        public Slot(int startTime, int endTime)
        {
            if (startTime >= endTime)
            {
                throw new ArgumentException("Start time must be before end time");
            }
            if (startTime< 0 || endTime< 0 || endTime> 1440 || startTime> 1440)
            {
                throw new ArgumentException("Time must be between 0 and 1440");
            }

            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Slot s = (Slot)obj;
            return s.StartTime == StartTime && s.EndTime == EndTime;
        }

        public override int GetHashCode()
        {
            return StartTime.GetHashCode() ^ EndTime.GetHashCode();
        }
    }

}