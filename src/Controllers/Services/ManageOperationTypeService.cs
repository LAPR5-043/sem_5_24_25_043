using AppContext= src.Models.AppContext;

namespace src.Controllers.Services;

public class ManageOperationTypeService
{
    private readonly AppContext _context;

    public ManageOperationTypeService( AppContext context)
    {
        _context = context;
    }
    
    public bool deactivateOperationType(long operationTypeID){
        return true;      
    }
    
    public bool deactivateOperationType(long operationTypeID, bool isActive){

        return true;
    }
}