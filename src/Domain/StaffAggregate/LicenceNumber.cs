public class LicenseNumber
{
    public long licenseNumber { get; }
    
    public LicenseNumber(long value)
    {
        if (value < 0){
            throw new ArgumentException("License number cannot be negative.");
        }
        licenseNumber = value;
    }
}