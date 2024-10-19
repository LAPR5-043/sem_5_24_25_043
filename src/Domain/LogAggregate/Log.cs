using NuGet.Packaging.Signing;
using src.Domain.Shared;

public class Log : Entity<LongId>
{
    public LongId logId { get;  set; }
    public string action { get;  set;}
    public DateTime timestamp { get;  set; }
    public string email { get;  set; }

    public Log(string action, DateTime timestamp, string email)
    {
        this.action = action;
        this.timestamp = timestamp;
        this.email = email;
    }

    public Log()
    {
        
    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Log log = (Log)obj;
        return action == log.action && timestamp == log.timestamp && email == log.email;
    }

    public override int GetHashCode() {
        return action.GetHashCode() + timestamp.GetHashCode() + email.GetHashCode();
    }

    public override string ToString()
    {
        return action + " " + timestamp.ToShortTimeString() + " " + email;
    }   

}

