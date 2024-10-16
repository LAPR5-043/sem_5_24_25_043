using Microsoft.EntityFrameworkCore;
using src.Infrastructure;
using src.Infrastructure.OperationTypes;

public class Repositories {

    public static OperationTypeRepository operationTypeRepository = new OperationTypeRepository(  new DDDSample1DbContext(new DbContextOptionsBuilder<DDDSample1DbContext>().UseInMemoryDatabase("Test").Options));



    public OperationTypeRepository getOperationTypeRepository()
    {
        return operationTypeRepository;
    }    




}