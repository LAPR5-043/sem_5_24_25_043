using Domain.OperationRequestAggregate;
using Domain.PatientAggregate;
using Infrastructure.PatientRepository;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Sempi5.Infrastructure.StaffRepository;
using src.Domain;
using src.Domain.AppointmentAggregate;
using src.Domain.SurgeryRoomAggregate;

namespace src.Models;

public class AppContext : DbContext
{
    public AppContext()
    { }
    public AppContext(DbContextOptions<AppContext> options)
        : base(options)
    {
    }

    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Patient> Patients { get; set; }
    //public DbSet<Appointment> Appointments { get; set; } = null!;
    //public DbSet<OperationRequest> OperationRequests { get; set; } = null!;
    //public DbSet<OperationType> OperationTypes { get; set; } = null!;
    //public DbSet<Specialization> Specializations { get; set; } = null!;
    //public DbSet<SurgeryRoom> SurgeryRooms { get; set; } = null!;

  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Staff>(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Patient>(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Log>(new LogEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);

    }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer( "Server=vsgate-s1.dei.isep.ipp.pt,11366;Database=MedOptima;User Id=MedOptima;Password=Medoptima;MultipleActiveResultSets=true;TrustServerCertificate=True", sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        }
    }

}