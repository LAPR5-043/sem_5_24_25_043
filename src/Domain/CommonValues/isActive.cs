public class IsActive
{
    public bool active { get; private set;}

    public IsActive() {}

    public IsActive(bool isActive) {  
        active = isActive;
    }

    public bool isActive() {
        return active;
    }

    public override string ToString()
    {
        return ""+active+"";
    }
}