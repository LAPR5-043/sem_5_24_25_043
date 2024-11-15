using Domain.OperationRequestAggregate;
using Domain.PatientAggregate;
using Infrastructure.PatientRepository;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using Sempi5.Infrastructure.StaffRepository;
using src.Domain;
using src.Domain.AppointmentAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain;
using src.Domain.SurgeryRoomAggregate;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using Domain.AppointmentAggregate;
using src.Domain.AvailabilitySlotAggregate;

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
    public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; } = null!;
    public DbSet<Specialization> Specializations { get; set; } = null!;
    public DbSet<SurgeryRoom> SurgeryRooms { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Staff>(new StaffEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Patient>(new PatientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Log>(new LogEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<OperationType>(new OperationTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<OperationRequest>(new OperationRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Appointment>(new AppointmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<PendingRequest>(new PendingRequestEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<AvailabilitySlot>(new AvailabilitySlotTypeConfiguration());
        modelBuilder.ApplyConfiguration<SurgeryRoom>(new SurgeryRoomEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration<Specialization>(new SpecializationEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
        var random = new Random();


        modelBuilder.Entity<Specialization>().HasData(
            new Specialization
            {
                Id = new SpecializationName("orthopaedist"),
                specializationName = new SpecializationName("orthopaedist"),
                specializationDescription = new SpecializationDescription("Orthopaedist")
            },
            new Specialization
            {
                Id = new SpecializationName("anaesthetist"),
                specializationName = new SpecializationName("anaesthetist"),
                specializationDescription = new SpecializationDescription("Anaesthetist")
            },
            new Specialization
            {
                Id = new SpecializationName("instrumenting"),
                specializationName = new SpecializationName("instrumenting"),
                specializationDescription = new SpecializationDescription("Instrumenting")
            },
            new Specialization
            {
                Id = new SpecializationName("circulating"),
                specializationName = new SpecializationName("circulating"),
                specializationDescription = new SpecializationDescription("Circulating")
            },
            new Specialization
            {
                Id = new SpecializationName("medical_action"),
                specializationName = new SpecializationName("medical_action"),
                specializationDescription = new SpecializationDescription("Medical Action")
            }

        );

        modelBuilder.Entity<SurgeryRoom>().HasData(
            new SurgeryRoom
            {
                Id = new RoomId("or1"),
                RoomID = new RoomId("or1"),
                Name = "Orthopedic Surgery Room 1"
            },
            new SurgeryRoom
            {
                Id = new RoomId("or2"),
                RoomID = new RoomId("or2"),
                Name = "Orthopedic Surgery Room 2"
            },
            new SurgeryRoom
            {
                Id = new RoomId("or3"),
                RoomID = new RoomId("or3"),
                Name = "Orthopedic Surgery Room 3"
            }

        );

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
                    Id = new StaffID("d202400001"),
                    staffID = new StaffID("d202400001"),
                    firstName = new StaffFirstName("John"),
                    lastName = new StaffLastName("Doe"),
                    fullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe")),
                    email = new StaffEmail("d202400001@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400001",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400002"),
                    staffID = new StaffID("d202400002"),
                    firstName = new StaffFirstName("Jane"),
                    lastName = new StaffLastName("Smith"),
                    fullName = new StaffFullName(new StaffFirstName("Jane"), new StaffLastName("Smith")),
                    email = new StaffEmail("d202400002@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400002",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("d202400003"),
                    staffID = new StaffID("d202400003"),
                    firstName = new StaffFirstName("Carlos"),
                    lastName = new StaffLastName("Moedas"),
                    fullName = new StaffFullName(new StaffFirstName("Carlos"), new StaffLastName("Moedas")),
                    email = new StaffEmail("d202400003@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400003",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400011"),
                    staffID = new StaffID("d202400011"),
                    firstName = new StaffFirstName("Maria"),
                    lastName = new StaffLastName("Silva"),
                    fullName = new StaffFullName(new StaffFirstName("Maria"), new StaffLastName("Silva")),
                    email = new StaffEmail("d202400011@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400011",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400012"),
                    staffID = new StaffID("d202400012"),
                    firstName = new StaffFirstName("Ana"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Ana"), new StaffLastName("Costa")),
                    email = new StaffEmail("d202400012@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400012",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400023"),
                    staffID = new StaffID("d202400023"),
                    firstName = new StaffFirstName("Luis"),
                    lastName = new StaffLastName("Martins"),
                    fullName = new StaffFullName(new StaffFirstName("Luis"), new StaffLastName("Martins")),
                    email = new StaffEmail("d202400023@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400023",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400024"),
                    staffID = new StaffID("n202400024"),
                    firstName = new StaffFirstName("Pedro"),
                    lastName = new StaffLastName("Gomes"),
                    fullName = new StaffFullName(new StaffFirstName("Pedro"), new StaffLastName("Gomes")),
                    email = new StaffEmail("n202400024@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400024",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400029"),
                    staffID = new StaffID("n202400029"),
                    firstName = new StaffFirstName("Sara"),
                    lastName = new StaffLastName("Ribeiro"),
                    fullName = new StaffFullName(new StaffFirstName("Sara"), new StaffLastName("Ribeiro")),
                    email = new StaffEmail("n202400029@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400029",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400022"),
                    staffID = new StaffID("n202400022"),
                    firstName = new StaffFirstName("David"),
                    lastName = new StaffLastName("Fernandes"),
                    fullName = new StaffFullName(new StaffFirstName("David"), new StaffLastName("Fernandes")),
                    email = new StaffEmail("n202400022@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400022",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400025"),
                    staffID = new StaffID("n202400025"),
                    firstName = new StaffFirstName("Laura"),
                    lastName = new StaffLastName("Sousa"),
                    fullName = new StaffFullName(new StaffFirstName("Laura"), new StaffLastName("Sousa")),
                    email = new StaffEmail("n202400025@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400025",
                    specializationID = "circulating"
                },
                new Staff
                {
                    Id = new StaffID("n202400030"),
                    staffID = new StaffID("n202400030"),
                    firstName = new StaffFirstName("John"),
                    lastName = new StaffLastName("Doe"),
                    fullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe")),
                    email = new StaffEmail("n202400030@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400030",
                    specializationID = "circulating"
                },
                new Staff
                {
                    Id = new StaffID("n202400031"),
                    staffID = new StaffID("n202400031"),
                    firstName = new StaffFirstName("Jane"),
                    lastName = new StaffLastName("Smith"),
                    fullName = new StaffFullName(new StaffFirstName("Jane"), new StaffLastName("Smith")),
                    email = new StaffEmail("n202400031@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400031",
                    specializationID = "circulating"
                },
                new Staff
                {
                    Id = new StaffID("n202400026"),
                    staffID = new StaffID("n202400026"),
                    firstName = new StaffFirstName("Carlos"),
                    lastName = new StaffLastName("Moedas"),
                    fullName = new StaffFullName(new StaffFirstName("Carlos"), new StaffLastName("Moedas")),
                    email = new StaffEmail("n202400026@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400026",
                    specializationID = "instrumenting"
                },
                new Staff
                {
                    Id = new StaffID("n202400027"),
                    staffID = new StaffID("n202400027"),
                    firstName = new StaffFirstName("Maria"),
                    lastName = new StaffLastName("Silva"),
                    fullName = new StaffFullName(new StaffFirstName("Maria"), new StaffLastName("Silva")),
                    email = new StaffEmail("n202400027@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400027",
                    specializationID = "instrumenting"
                },
                new Staff
                {
                    Id = new StaffID("n202400028"),
                    staffID = new StaffID("n202400028"),
                    firstName = new StaffFirstName("Ana"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Ana"), new StaffLastName("Costa")),
                    email = new StaffEmail("n202400028@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400028",
                    specializationID = "instrumenting"
                },
                new Staff
                {
                    Id = new StaffID("s202400001"),
                    staffID = new StaffID("s202400001"),
                    firstName = new StaffFirstName("Luis"),
                    lastName = new StaffLastName("Martins"),
                    fullName = new StaffFullName(new StaffFirstName("Luis"), new StaffLastName("Martins")),
                    email = new StaffEmail("s202400001@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "s202400001",
                    specializationID = "medical_action"
                }

        );

        modelBuilder.Entity<OperationRequest>().HasData(
                new OperationRequest
                {

                    Id = new OperationRequestID("1"),
                    operationRequestID = new OperationRequestID("1"),
                    patientID = "1",
                    doctorID = "D202400003",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400003","d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "D202400002","n202400022" } },
                        { "instrumenting", new List<string>() { "n202400026", } },
                        { "circulating", new List<string>() { "n202400030", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },

                new OperationRequest
                {

                    Id = new OperationRequestID("2"),
                    operationRequestID = new OperationRequestID("2"),
                    patientID = "1",
                    doctorID = "D202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400003","d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "D202400002","n202400022" } },
                        { "instrumenting", new List<string>() { "n202400026", } },
                        { "circulating", new List<string>() { "n202400030", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("3"),
                    operationRequestID = new OperationRequestID("3"),
                    patientID = "2",
                    doctorID = "d202400001",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400012" } },
                        { "anaesthetist", new List<string>() { "n202400024", "d202400002" } },
                        { "instrumenting", new List<string>() { "n202400026" } },
                        { "circulating", new List<string>() { "n202400025" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("4"),
                    operationRequestID = new OperationRequestID("4"),
                    patientID = "2",
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "d202400002","n202400024" } },
                        { "instrumenting", new List<string>() { "n202400027" } },
                        { "circulating", new List<string>() { "n202400031" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("5"),
                    operationRequestID = new OperationRequestID("5"),
                    patientID = "3",
                    doctorID = "d202400011",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "d202400023","n202400024" } },
                        { "instrumenting", new List<string>() { "n202400026" } },
                        { "circulating", new List<string>() { "n202400025" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("6"),
                    operationRequestID = new OperationRequestID("6"),
                    patientID = "3",
                    doctorID = "d202400003",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400003","d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "d202400002","n202400022" } },
                        { "instrumenting", new List<string>() { "n202400026" } },
                        { "circulating", new List<string>() { "n202400030" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("7"),
                    operationRequestID = new OperationRequestID("7"),
                    patientID = "4",
                    doctorID = "d202400003",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400003","d202400011","d202400001" } },
                        { "anaesthetist", new List<string>() { "d202400002","n202400029" } },
                        { "instrumenting", new List<string>() { "n202400026" } },
                        { "circulating", new List<string>() { "n202400025" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("8"),
                    operationRequestID = new OperationRequestID("8"),
                    patientID = "4",
                    doctorID = "d202400001",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400003","d202400012" } },
                        { "anaesthetist", new List<string>() { "d202400023","n202400024" } },
                        { "instrumenting", new List<string>() { "n202400026" } },
                        { "circulating", new List<string>() { "n202400025" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("9"),
                    operationRequestID = new OperationRequestID("9"),
                    patientID = "5",
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "d202400002","n202400024" } },
                        { "instrumenting", new List<string>() { "n202400027" } },
                        { "circulating", new List<string>() { "n202400031" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                },
                new OperationRequest
                {
                    Id = new OperationRequestID("10"),
                    operationRequestID = new OperationRequestID("10"),
                    patientID = "5",
                    doctorID = "d202400011",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 1, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400011","d202400012" } },
                        { "anaesthetist", new List<string>() { "d202400023","n202400024" } },
                        { "instrumenting", new List<string>() { "n202400026" } },
                        { "circulating", new List<string>() { "n202400025" } },
                        { "medical_action", new List<string>() { "s202400001" } }
                    }
                }
        );

        modelBuilder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = new AppointmentID("1"),
                appointmentID = new AppointmentID("1"),
                requestID = 1,
                roomID = "or1",
                dateAndTime = new DateAndTime("20241028", "720", "1200"),
                status = Status.Scheduled
            }
        );

        modelBuilder.Entity<AvailabilitySlot>().HasData(
            new AvailabilitySlot[]  {
                new AvailabilitySlot
                {
                    Id = new StaffID("d202400001"),
                    StaffID = new StaffID("d202400001"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("d202400002"),
                    StaffID = new StaffID("d202400002"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(500, 1440) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("d202400003"),
                    StaffID = new StaffID("d202400003"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(520, 1320) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("d202400011"),
                    StaffID = new StaffID("d202400011"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("d202400012"),
                    StaffID = new StaffID("d202400012"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("d202400023"),
                    StaffID = new StaffID("d202400023"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400024"),
                    StaffID = new StaffID("n202400024"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1300) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400029"),
                    StaffID = new StaffID("n202400029"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1400) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400022"),
                    StaffID = new StaffID("n202400022"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400025"),
                    StaffID = new StaffID("n202400025"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1300) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400030"),
                    StaffID = new StaffID("n202400030"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1400) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400031"),
                    StaffID = new StaffID("n202400031"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400026"),
                    StaffID = new StaffID("n202400026"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400027"),
                    StaffID = new StaffID("n202400027"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1300) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("n202400028"),
                    StaffID = new StaffID("n202400028"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1200) }
                    }
                },
                new AvailabilitySlot
                {
                    Id = new StaffID("s202400001"),
                    StaffID = new StaffID("s202400001"),
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20241028, new Slot(480, 1400) }
                    }
                }
            }          
        );

        modelBuilder.Entity<OperationType>().HasData(

            new OperationType
            {
                Id = new OperationTypeName("so2"),
                operationTypeName = new OperationTypeName("so2"),
                operationTypeDescription = new OperationTypeDescription("Knee Replacement Surgery"),
                estimatedDuration = new EstimatedDuration(45,60,45),
                isActive = true,
                specializations = new Dictionary<string, int>()
                {
                    { "d;orthopaedist", 3 },
                    { "d;anaesthetist", 1 },
                    { "n;circulating", 1 },
                    { "n;instrumenting", 1 },
                    { "n;anaesthetist", 1 },
                    { "s;medical_action", 1 }

                }
            },

            new OperationType
            {
                Id = new OperationTypeName("so3"),
                operationTypeName = new OperationTypeName("so3"),
                operationTypeDescription = new OperationTypeDescription("Shoulder Replacement Surgery"),
                estimatedDuration = new EstimatedDuration(45,90,45),
                isActive = true,
                specializations = new Dictionary<string, int>()
                {
                    { "d;orthopaedist", 3 },
                    { "d;anaesthetist", 1 },
                    { "n;circulating", 1 },
                    { "n;instrumenting", 1 },
                    { "n;anaesthetist", 1 },
                    { "s;medical_action", 1 }

                }
            },

            new OperationType
            {
                Id = new OperationTypeName("so4"),
                operationTypeName = new OperationTypeName("so4"),
                operationTypeDescription = new OperationTypeDescription("Hip Replacement Surgery"),
                estimatedDuration = new EstimatedDuration(45,75,45),
                isActive = true,
                specializations = new Dictionary<string, int>()
                {
                    { "d;orthopaedist", 2 },
                    { "d;anaesthetist", 1 },
                    { "n;circulating", 1 },
                    { "n;instrumenting", 1 },
                    { "n;anaesthetist", 1 },
                    { "s;medical_action", 1 }

                }
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
