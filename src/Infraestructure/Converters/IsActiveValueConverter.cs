using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class IsActiveValueConverter : ValueConverter<bool, int>
{
    public IsActiveValueConverter() 
        : base(
            isActive => isActive ? 1 : 0, 
            value => value == 1)
    {
    }
}