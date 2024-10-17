using NuGet.Packaging.Signing;
using src.Domain.Shared;

public class Log 
{
    public long logId { get; private set; }
    public string action { get; private set;}
    public Timestamp timestamp { get; private set; }
    public string email { get; private set; }

    public Log(string action, Timestamp timestamp, string email)
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
        return action + " " + timestamp + " " + email;
    }   

}

