using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


public class OperationTypeIDValueConverter : ValueConverter<OperationTypeID, long>
{
    public OperationTypeIDValueConverter() 
        : base(
            id => id.GetId(), 
            value => new OperationTypeID(value))
    {
    }
}