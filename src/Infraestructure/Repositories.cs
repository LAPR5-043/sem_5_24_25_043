using Microsoft.EntityFrameworkCore;
using src.Infrastructure;
using src.Infrastructure.OperationTypes;
using AppContext = src.Models.AppContext;

public class Repositories {


    private static Repositories _instance;
    private static readonly object _lock = new object();
    private static AppContext appContext = new AppContext(new DbContextOptionsBuilder<AppContext>().Options);
    public static OperationTypeRepository operationTypeRepository = new OperationTypeRepository(appContext);
    public static SpecializationRepository specializationRepository = new SpecializationRepository(appContext);
    public static StaffRepository staffRepository = new StaffRepository(appContext);
    private Repositories()
    {
    }

    public static Repositories GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Repositories();
                }
            }
        }
        return _instance;
    }


    public OperationTypeRepository getOperationTypeRepository()
    {
        return operationTypeRepository;
    }    


    public StaffRepository getStaffRepository()
    {   
        return staffRepository;
    }

    public SpecializationRepository getSpecializationRepository()
    {   
        return specializationRepository;
    }

    



}