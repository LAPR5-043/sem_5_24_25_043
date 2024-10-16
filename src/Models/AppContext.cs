using Microsoft.EntityFrameworkCore;
using src.Domain;

namespace src.Models;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public DbSet<OperationType> OperationTypes { get; set; } = null!;

}