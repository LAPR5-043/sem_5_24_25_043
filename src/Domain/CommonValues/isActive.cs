public class IsActive
{
    private bool active { get; }
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