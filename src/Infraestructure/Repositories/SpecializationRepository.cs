using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;


public class SpecializationRepository : BaseRepository<Specialization, SpecializationName>
{
    public SpecializationRepository(DbSet<Specialization> objs) : base(objs)
    {
        
    }
}