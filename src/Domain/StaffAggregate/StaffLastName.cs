using src.Domain.Shared;

public class StaffLastName : IValueObject{

    public string lastName { get; }

    public StaffLastName(string lastName){
        if (string.IsNullOrWhiteSpace(lastName)){
            throw new System.ArgumentException("First Name cannot be null or empty");
        }
        if(!lastName.All(char.IsLetter)){
            throw new System.ArgumentException("First Name must contain only letters");
        }
        this.lastName = lastName;
    }
    public StaffLastName(){
   
    }

    public override bool Equals(object obj){
        if(obj == null || GetType() != obj.GetType()){
            return false;
        }

        StaffLastName staffFirstName = (StaffLastName)obj;
        return lastName == staffFirstName.lastName;

    }
    public override int GetHashCode() { 
        return lastName.GetHashCode();
    }
    public override string ToString()
    {
        return lastName;
    }
}