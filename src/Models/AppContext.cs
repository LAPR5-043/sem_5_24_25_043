using Domain.OperationRequestAggregate;
using Domain.PatientAggregate;
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

    public DbSet<Staff> Staffs { get; set; } = null!;
    //public DbSet<Appointment> Appointments { get; set; } = null!;
    //public DbSet<OperationRequest> OperationRequests { get; set; } = null!;
    //public DbSet<OperationType> OperationTypes { get; set; } = null!;
    //public DbSet<Patient> Patients { get; set; } = null!;
    //public DbSet<Specialization> Specializations { get; set; } = null!;
    //public DbSet<SurgeryRoom> SurgeryRooms { get; set; } = null!;

  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StaffEntityTypeConfiguration());
        // Seed data
        string cardiologyId = "Cardiology";
        string neurologyId = "Neurology";

       /*modelBuilder.Entity<Specialization>().HasData(
            new Specialization("Cardiology", "Heart specialist") { Id = new SpecializationName(cardiologyId) },
            new Specialization("Neurology", "Brain specialist") { Id = new SpecializationName(neurologyId) }
        );*/ 

    
       
        //modelBuilder.Entity<Staff>().HasData(


    }

}