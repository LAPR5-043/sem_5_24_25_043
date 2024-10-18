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
    //public DbSet<Appointment> Appointments { get; set; } = null!;
    //public DbSet<OperationRequest> OperationRequests { get; set; } = null!;
    //public DbSet<OperationType> OperationTypes { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; }
    //public DbSet<Specialization> Specializations { get; set; } = null!;
    //public DbSet<SurgeryRoom> SurgeryRooms { get; set; } = null!;

  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Staff>(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Patient>(new PatientEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);

    }

}