using Domain.OperationRequestAggregate;
using Domain.PatientAggregate;
using Infrastructure.PatientRepository;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using Sempi5.Infrastructure.StaffRepository;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain;
using src.Domain.AppointmentAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain.PatientAggregate;
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
    public DbSet<PendingRequest> PendingRequests { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<OperationType> OperationTypes { get; set; } = null!;

    //public DbSet<Appointment> Appointments { get; set; } = null!;
    public DbSet<OperationRequest> OperationRequests { get; set; } = null!;
    //public DbSet<Specialization> Specializations { get; set; } = null!;
    //public DbSet<SurgeryRoom> SurgeryRooms { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Staff>(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Patient>(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Log>(new LogEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<OperationType>(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<OperationRequest>(new OperationRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<PendingRequest>(new PendingRequestEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = new MedicalRecordNumber("1"),
                    MedicalRecordNumber = new MedicalRecordNumber("1"),
                    FirstName = new PatientFirstName("John"),
                    LastName = new PatientLastName("Doe"),
                    FullName = new PatientFullName("John", "Doe"),
                    Email = new PatientEmail("john@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919919919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919919919"),
                    DateOfBirth = new DateOfBirth("01", "01", "1999"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("2"),
                    MedicalRecordNumber = new MedicalRecordNumber("2"),
                    FirstName = new PatientFirstName("Jane"),
                    LastName = new PatientLastName("Does"),
                    FullName = new PatientFullName("Jane", "Does"),
                    Email = new PatientEmail("Jane@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919991919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth("01", "01", "1999"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                }
            );

        modelBuilder.Entity<OperationRequest>().HasData(
                new OperationRequest
                {
                    
                    Id = new OperationRequestID("1"),
                    operationRequestID = new OperationRequestID("1"),
                    patientID = 1,
                    doctorID = "s202400001",
                    operationTypeID = "Knee Surgery",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("2"),
                    operationRequestID = new OperationRequestID("2"),
                    patientID = 2,
                    doctorID = "s202400002",
                    operationTypeID = "Heart Surgery",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Effective
                }
        );
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
