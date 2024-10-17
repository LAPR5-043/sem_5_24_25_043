using src.Domain.Shared;
using Microsoft.EntityFrameworkCore;
[Owned]
public class StaffFirstName  : IValueObject{

    public string firstName { get; }

    public StaffFirstName(string firstName){
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