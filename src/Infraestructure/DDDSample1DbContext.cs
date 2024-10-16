using Microsoft.EntityFrameworkCore;
using src.Infrastructure.OperationTypes;


namespace src.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DbSet<OperationType> OperationTypes { get; set; }


        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());

        }
    }
}