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
    public DbSet<Specialization> Specializations { get; set; } = null!;
    public DbSet<Staff> Staffs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SpecializationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());

        // Seed data
        string cardiologyId = "Cardiology";
        string neurologyId = "Neurology";

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization("Cardiology", "Heart specialist") { Id = new SpecializationName(cardiologyId) },
            new Specialization("Neurology", "Brain specialist") { Id = new SpecializationName(neurologyId) }
        );

        modelBuilder.Entity<Staff>().HasData(
            new Staff("John", "Doe", "N202100001", true, "923456890", "johndoe@example.com", cardiologyId, null) { Id = new LicenseNumber("N202100001") } ,
            new Staff("Jane", "Smith", "D202100002", true, "987654210", "janesmith@example.com", neurologyId, null) { Id = new LicenseNumber("D202100002") });
  

    }

}