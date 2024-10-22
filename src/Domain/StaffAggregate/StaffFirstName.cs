using src.Domain.Shared;
public class StaffFirstName  : IValueObject{

    public string firstName { get; }

    public StaffFirstName(string firstName){

        if (string.IsNullOrWhiteSpace(firstName)){
            throw new System.ArgumentException("First Name cannot be null or empty");
        }
        if(!firstName.All(char.IsLetter)){
            throw new System.ArgumentException("First Name must contain only letters");
        }
        this.firstName = firstName;
    }
    public StaffFirstName(){
   
    }

    public override bool Equals(object obj){
        if(obj == null || GetType() != obj.GetType()){
            return false;
        }

        StaffFirstName staffFirstName = (StaffFirstName)obj;
        return firstName == staffFirstName.firstName;

    }
    public override int GetHashCode() { 
        return firstName.GetHashCode();
    }
    public override string ToString()
    {
        return firstName;
    }

}