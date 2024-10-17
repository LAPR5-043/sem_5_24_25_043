using Microsoft.EntityFrameworkCore;
using src.Domain;

namespace src.Models;

public class AppContext : DbContext
{
    public AppContext()
    {
    }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    //public DbSet<OperationType> OperationTypes { get; set; } = null!;
    public DbSet<Specialization> Specializations { get; set; } = null!;
    public DbSet<Staff> Staffs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Seed data
        string cardiologyId = "Cardiology";
        string neurologyId = "Neurology";

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization("Cardiology", "Heart specialist") { Id = new SpecializationName(cardiologyId) },
            new Specialization("Neurology", "Brain specialist") { Id = new SpecializationName(neurologyId) }
        );

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.staffID);
            entity.Property(e => e.firstName).IsRequired();
            entity.Property(e => e.lastName).IsRequired();
            entity.Property(e => e.email).IsRequired();
            entity.Property(e => e.phoneNumber).IsRequired();
            entity.Property(e => e.licenseNumber).IsRequired();
            entity.Property(e => e.isActive).IsRequired();
            entity.Property(e => e.specializationID).IsRequired();

            // Configure the constructor binding
            entity.HasData(
                new Staff("1", "John", "Doe", "john.doe@example.com", "1234567890", "LN12345", true, "SPEC001")
            );
        });
        //modelBuilder.Entity<Staff>().HasData(


    }

}