using src.Domain.Shared;
using Microsoft.EntityFrameworkCore;
[Owned]
public class StaffLastName : IValueObject{

    public string lastName { get; }

    public StaffLastName(string lastName){
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