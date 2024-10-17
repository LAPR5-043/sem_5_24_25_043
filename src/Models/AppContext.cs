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

        //modelBuilder.Entity<Staff>().HasData(


    }

}