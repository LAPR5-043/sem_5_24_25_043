using Microsoft.EntityFrameworkCore;
using src.Domain;
using src.Infrastructure.OperationTypes;

namespace src.Models;

public class AppContext : DbContext
{
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public DbSet<OperationType> OperationTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());

    }

}