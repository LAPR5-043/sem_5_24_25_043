using src.Domain.Shared;

public class LicenseNumber : IValueObject {

    public string licenseNumber { get; }

    public LicenseNumber(string licenseNumber) {
        this.licenseNumber = licenseNumber;

    }

    public LicenseNumber() {
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        LicenseNumber licenseNumber = (LicenseNumber)obj;
        return this.licenseNumber == licenseNumber.licenseNumber;
    }

    public override int GetHashCode() { 
        return licenseNumber.GetHashCode();
    }

    public override string ToString() {
        return licenseNumber;
    }

}