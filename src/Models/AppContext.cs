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
using sem_5_24_25_043.Domain.AppointmentAggregate;
using Domain.AppointmentAggregate;


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

    public DbSet<Appointment> Appointments { get; set; } = null!;
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
        modelBuilder.ApplyConfiguration<Appointment>(new AppointmentEntityTypeConfiguration());
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

        modelBuilder.Entity<Staff>().HasData(
            new Staff
            {
                Id = new StaffID("D202400001"),
                staffID = new StaffID("D202400001"),
                firstName = new StaffFirstName("John"),
                lastName = new StaffLastName("Doe"),
                fullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe")),
                email = new StaffEmail("d123@doctor.com"),
                phoneNumber = new StaffPhoneNumber("+351919919919"),
                licenseNumber = new LicenseNumber("123456"),
                isActive = true,
                availabilitySlots = new AvailabilitySlots(),
                specializationID = "Cardiology"
            },

            new Staff
            {
                Id = new StaffID("D202400011"),
                staffID = new StaffID("D202400011"),
                firstName = new StaffFirstName("Carlos"),
                lastName = new StaffLastName("Moedas"),
                fullName = new StaffFullName(new StaffFirstName("Carlos"), new StaffLastName("Moedas")),
                email = new StaffEmail("D202400011@medopt.com"),
                phoneNumber = new StaffPhoneNumber("+351919911319"),
                licenseNumber = new LicenseNumber("121236"),
                isActive = true,
                availabilitySlots = new AvailabilitySlots(),
                specializationID = "Orthopedics"
            }

        );

        modelBuilder.Entity<OperationRequest>().HasData(
                new OperationRequest
                {

                    Id = new OperationRequestID("1"),
                    operationRequestID = new OperationRequestID("1"),
                    patientID = 1,
                    doctorID = "D202400001",
                    operationTypeID = "Knee Surgery",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("2"),
                    operationRequestID = new OperationRequestID("2"),
                    patientID = 2,
                    doctorID = "D202400001",
                    operationTypeID = "Heart Surgery",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Effective
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("3"),
                    operationRequestID = new OperationRequestID("3"),
                    patientID = 2,
                    doctorID = "s202400002",
                    operationTypeID = "Heart Surgery",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Effective
                }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = new AppointmentID("1"),
                appointmentID = new AppointmentID("1"),
                requestID = 1,
                roomID = 1,
                dateAndTime = new DateAndTime(DateTime.Now),
                status = Status.Scheduled
            }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=vsgate-s1.dei.isep.ipp.pt,11366;Database=MedOptima;User Id=MedOptima;Password=Medoptima;MultipleActiveResultSets=true;TrustServerCertificate=True", sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        }
    }

}
