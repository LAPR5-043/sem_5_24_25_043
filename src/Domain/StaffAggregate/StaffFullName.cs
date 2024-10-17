using src.Domain.Shared;

public class StaffFullName  : IValueObject {
    public string fullName { get; }
    
    public StaffFullName(StaffFirstName firstName, StaffLastName lastName) {
        this.fullName = firstName.ToString() + " " + lastName.ToString();
    }

    public StaffFullName() {
    }

    public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        StaffFullName staffFullName = (StaffFullName)obj;
        return fullName == staffFullName.fullName;
    }

    public override int GetHashCode() { 
        return fullName.GetHashCode();
    }

    public override string ToString(){
        return fullName;
    }
}
    