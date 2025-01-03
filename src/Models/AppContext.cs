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
            },

            new SurgeryRoom
            {
                Id = new RoomId("or4"),
                RoomID = new RoomId("or4"),
                Name = "Orthopedic Surgery Room 4"
            },
            new SurgeryRoom
            {
                Id = new RoomId("or5"),
                RoomID = new RoomId("or5"),
                Name = "Orthopedic Surgery Room 5"
            },
            new SurgeryRoom
            {
                Id = new RoomId("or6"),
                RoomID = new RoomId("or6"),
                Name = "Orthopedic Surgery Room 6"
            }

        );
        List<Patient> patients = new List<Patient>(){
                new Patient
                {
                    Id = new MedicalRecordNumber("202310056123"),
                    MedicalRecordNumber = new MedicalRecordNumber("202310056123"),
                    FirstName = new PatientFirstName("John"),
                    LastName = new PatientLastName("Doe"),
                    FullName = new PatientFullName("John", "Doe"),
                    Email = new PatientEmail("john@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919919919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919919919"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410007891"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410007891"),
                    FirstName = new PatientFirstName("Jane"),
                    LastName = new PatientLastName("Does"),
                    FullName = new PatientFullName("Jane", "Does"),
                    Email = new PatientEmail("Jane@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919991919"),
                    EmergencyContact = new EmergencyContact("Carlos", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410007911"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410007911"),
                    FirstName = new PatientFirstName("João"),
                    LastName = new PatientLastName("Silva"),
                    FullName = new PatientFullName("João", "Silva"),
                    Email = new PatientEmail("silva@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351911382919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410120782"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410120782"),
                    FirstName = new PatientFirstName("André"),
                    LastName = new PatientLastName("Costa"),
                    FullName = new PatientFullName("André", "Costa"),
                    Email = new PatientEmail("costa@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351911011919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410033891"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410033891"),
                    FirstName = new PatientFirstName("Joana"),
                    LastName = new PatientLastName("Silva"),
                    FullName = new PatientFullName("Joana", "Silva"),
                    Email = new PatientEmail("joanac@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351911231919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410019271"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410019271"),
                    FirstName = new PatientFirstName("Diogo"),
                    LastName = new PatientLastName("Neto"),
                    FullName = new PatientFullName("Diogo", "Neto"),
                    Email = new PatientEmail("diogonet@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919994919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410555891"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410555891"),
                    FirstName = new PatientFirstName("Francisca"),
                    LastName = new PatientLastName("Lopes"),
                    FullName = new PatientFullName("Francisca", "Lopes"),
                    Email = new PatientEmail("franlopes@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919991072"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000941"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000941"),
                    FirstName = new PatientFirstName("Miguel"),
                    LastName = new PatientLastName("Rios"),
                    FullName = new PatientFullName("Miguel", "Rios"),
                    Email = new PatientEmail("rmiguel@email.com"),
                    PhoneNumber = new PatientPhoneNumber("+351919108919"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000942"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000942"),
                    FirstName = new PatientFirstName("Miguel"),
                    LastName = new PatientLastName("Santos"),
                    FullName = new PatientFullName("Miguel", "Santos"),
                    Email = new PatientEmail("smiguel@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000943"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000943"),
                    FirstName = new PatientFirstName("Carlos"),
                    LastName = new PatientLastName("Prada"),
                    FullName = new PatientFullName("Carlos", "Prada"),
                    Email = new PatientEmail("rrada@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000944"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000944"),
                    FirstName = new PatientFirstName("José"),
                    LastName = new PatientLastName("Furtado"),
                    FullName = new PatientFullName("José", "Furtado"),
                    Email = new PatientEmail("furtado@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000945"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000945"),
                    FirstName = new PatientFirstName("Diana"),
                    LastName = new PatientLastName("Rocha"),
                    FullName = new PatientFullName("Diana", "Rocha"),
                    Email = new PatientEmail("ochal@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000946"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000946"),
                    FirstName = new PatientFirstName("Jéssica"),
                    LastName = new PatientLastName("Vanessa"),
                    FullName = new PatientFullName("Jéssica", "Vanessa"),
                    Email = new PatientEmail("nessa@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000947"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000947"),
                    FirstName = new PatientFirstName("Andreia"),
                    LastName = new PatientLastName("Rios"),
                    FullName = new PatientFullName("Andreia", "Rios"),
                    Email = new PatientEmail("dreia@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000948"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000948"),
                    FirstName = new PatientFirstName("Mariana"),
                    LastName = new PatientLastName("Ribeiro"),
                    FullName = new PatientFullName("Mariana", "Ribeiro"),
                    Email = new PatientEmail("biggle@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000949"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000949"),
                    FirstName = new PatientFirstName("Matilde"),
                    LastName = new PatientLastName("Chaves"),
                    FullName = new PatientFullName("Matilde", "Chaves"),
                    Email = new PatientEmail("key@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000950"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000950"),
                    FirstName = new PatientFirstName("Isabel"),
                    LastName = new PatientLastName("Areal"),
                    FullName = new PatientFullName("Isabel", "Areal"),
                    Email = new PatientEmail("isaa@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000951"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000951"),
                    FirstName = new PatientFirstName("Noélia"),
                    LastName = new PatientLastName("Anjos"),
                    FullName = new PatientFullName("Noélia", "Anjos"),
                    Email = new PatientEmail("angel@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },
                new Patient
                {
                    Id = new MedicalRecordNumber("202410000952"),
                    MedicalRecordNumber = new MedicalRecordNumber("202410000952"),
                    FirstName = new PatientFirstName("Ermelinda"),
                    LastName = new PatientLastName("Barbosa"),
                    FullName = new PatientFullName("Ermelinda", "Barbosa"),
                    Email = new PatientEmail("minda@email.com"),
                    PhoneNumber = new PatientPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),
                    Gender = Gender.Female,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                },

                new Patient
                {
                    Id = new MedicalRecordNumber("202310001975"),
                    MedicalRecordNumber = new MedicalRecordNumber("202310001975"),
                    FirstName = new PatientFirstName("Ricky"),
                    LastName = new PatientLastName("Simons"),
                    FullName = new PatientFullName("Ricky", "Simons"),
                    Email = new PatientEmail("1220606@isep.ipp.pt"),
                    PhoneNumber = new PatientPhoneNumber("+351913613541"),
                    EmergencyContact = new EmergencyContact("Jane", "+351919999119"),
                    DateOfBirth = new DateOfBirth($"{random.Next(1, 30)}", $"{random.Next(1, 12):D2}", $"{random.Next(1969, 2020)}"),                    Gender = Gender.Male,
                    AllergiesAndConditions = new List<AllergiesAndConditions>(),
                    AppointmentHistory = new AppointmentHistory()
                }};
        modelBuilder.Entity<Patient>().HasData( patients
            );
        List<Staff> staffs = new List<Staff>(){
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
                    specializationID = "orthopaedist"
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
                    Id = new StaffID("d202400004"),
                    staffID = new StaffID("d202400004"),
                    firstName = new StaffFirstName("Maria"),
                    lastName = new StaffLastName("Silva"),
                    fullName = new StaffFullName(new StaffFirstName("Maria"), new StaffLastName("Silva")),
                    email = new StaffEmail("d202400004@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400004",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400005"),
                    staffID = new StaffID("d202400005"),
                    firstName = new StaffFirstName("Ana"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Ana"), new StaffLastName("Costa")),
                    email = new StaffEmail("d202400005@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400005",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400006"),
                    staffID = new StaffID("d202400006"),
                    firstName = new StaffFirstName("Luis"),
                    lastName = new StaffLastName("Martins"),
                    fullName = new StaffFullName(new StaffFirstName("Luis"), new StaffLastName("Martins")),
                    email = new StaffEmail("d202400006@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400006",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400007"),
                    staffID = new StaffID("d202400007"),
                    firstName = new StaffFirstName("Pedro"),
                    lastName = new StaffLastName("Gomes"),
                    fullName = new StaffFullName(new StaffFirstName("Pedro"), new StaffLastName("Gomes")),
                    email = new StaffEmail("d202400007@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400007",
                    specializationID = "orthopaedist"
                },
                new Staff
                {
                    Id = new StaffID("d202400008"),
                    staffID = new StaffID("d202400008"),
                    firstName = new StaffFirstName("Sara"),
                    lastName = new StaffLastName("Ribeiro"),
                    fullName = new StaffFullName(new StaffFirstName("Sara"), new StaffLastName("Ribeiro")),
                    email = new StaffEmail("d202400008@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400008",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("d202400009"),
                    staffID = new StaffID("d202400009"),
                    firstName = new StaffFirstName("David"),
                    lastName = new StaffLastName("Fernandes"),
                    fullName = new StaffFullName(new StaffFirstName("David"), new StaffLastName("Fernandes")),
                    email = new StaffEmail("d202400009@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400009",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("d202400010"),
                    staffID = new StaffID("d202400010"),
                    firstName = new StaffFirstName("Laura"),
                    lastName = new StaffLastName("Sousa"),
                    fullName = new StaffFullName(new StaffFirstName("Laura"), new StaffLastName("Sousa")),
                    email = new StaffEmail("d202400010@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "d202400010",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400011"),
                    staffID = new StaffID("n202400011"),
                    firstName = new StaffFirstName("John"),
                    lastName = new StaffLastName("Doe"),
                    fullName = new StaffFullName(new StaffFirstName("John"), new StaffLastName("Doe")),
                    email = new StaffEmail("n202400011@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400011",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400012"),
                    staffID = new StaffID("n202400012"),
                    firstName = new StaffFirstName("Jane"),
                    lastName = new StaffLastName("Smith"),
                    fullName = new StaffFullName(new StaffFirstName("Jane"), new StaffLastName("Smith")),
                    email = new StaffEmail("n202400012@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400012",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400013"),
                    staffID = new StaffID("n202400013"),
                    firstName = new StaffFirstName("Carlos"),
                    lastName = new StaffLastName("Moedas"),
                    fullName = new StaffFullName(new StaffFirstName("Carlos"), new StaffLastName("Moedas")),
                    email = new StaffEmail("n202400013@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400013",
                    specializationID = "anaesthetist"
                },
                new Staff
                {
                    Id = new StaffID("n202400014"),
                    staffID = new StaffID("n202400014"),
                    firstName = new StaffFirstName("Maria"),
                    lastName = new StaffLastName("Silva"),
                    fullName = new StaffFullName(new StaffFirstName("Maria"), new StaffLastName("Silva")),
                    email = new StaffEmail("n202400014@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400014",
                    specializationID = "instrumenting"
                },
                new Staff
                {
                    Id = new StaffID("n202400015"),
                    staffID = new StaffID("n202400015"),
                    firstName = new StaffFirstName("Nádia"),
                    lastName = new StaffLastName("Silva"),
                    fullName = new StaffFullName(new StaffFirstName("Nádia"), new StaffLastName("Silva")),
                    email = new StaffEmail("n202400015@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400015",
                    specializationID = "instrumenting"
                },
                new Staff
                {
                    Id = new StaffID("n202400016"),
                    staffID = new StaffID("n202400016"),
                    firstName = new StaffFirstName("José"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("José"), new StaffLastName("Costa")),
                    email = new StaffEmail("n202400016@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400016",
                    specializationID = "instrumenting"
                },
                new Staff
                {
                    Id = new StaffID("n202400017"),
                    staffID = new StaffID("n202400017"),
                    firstName = new StaffFirstName("Diogo"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Diogo"), new StaffLastName("Costa")),
                    email = new StaffEmail("n202400017@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400017",
                    specializationID = "circulating"
                },
                new Staff
                {
                    Id = new StaffID("n202400018"),
                    staffID = new StaffID("n202400018"),
                    firstName = new StaffFirstName("Arménio"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Arménio"), new StaffLastName("Costa")),
                    email = new StaffEmail("n202400018@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400018",
                    specializationID = "circulating"
                },
                new Staff
                {
                    Id = new StaffID("n202400019"),
                    staffID = new StaffID("n202400019"),
                    firstName = new StaffFirstName("Nininho"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Nininho"), new StaffLastName("Costa")),
                    email = new StaffEmail("n202400019@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "n202400019",
                    specializationID = "circulating"
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
                },
                new Staff
                {
                    Id = new StaffID("s202400002"),
                    staffID = new StaffID("s202400002"),
                    firstName = new StaffFirstName("Cândido"),
                    lastName = new StaffLastName("Costa"),
                    fullName = new StaffFullName(new StaffFirstName("Cândido"), new StaffLastName("Costa")),
                    email = new StaffEmail("s202400002@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "s202400002",
                    specializationID = "medical_action"
                },
                new Staff
                {
                    Id = new StaffID("s202400003"),
                    staffID = new StaffID("s202400003"),
                    firstName = new StaffFirstName("Reinaldo"),
                    lastName = new StaffLastName("Teles"),
                    fullName = new StaffFullName(new StaffFirstName("Reinaldo"), new StaffLastName("Teles")),
                    email = new StaffEmail("s202400003@medopt.com"),
                    phoneNumber = new StaffPhoneNumber($"+351{random.Next(910000000, 999999999)}"),
                    licenseNumber = new LicenseNumber(random.Next(100000, 999999).ToString()),
                    isActive = true,
                    availabilitySlotsID = "s202400003",
                    specializationID = "medical_action"
                }};

        modelBuilder.Entity<Staff>().HasData(staffs);
        

            List<OperationRequest> operationRequests = new List<OperationRequest>(){
                new OperationRequest
                {

                    Id = new OperationRequestID("1"),
                    operationRequestID = new OperationRequestID("1"),
                    patientID =((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400002","d202400003" } },
                        { "anaesthetist", new List<string>() { "d202400008","n202400011" } },
                        { "instrumenting", new List<string>() { "n202400014", } },
                        { "circulating", new List<string>() { "n202400017", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("2"),
                    operationRequestID = new OperationRequestID("2"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400002","d202400003" } },
                        { "anaesthetist", new List<string>() { "d202400008","n202400011" } },
                        { "instrumenting", new List<string>() { "n202400014", } },
                        { "circulating", new List<string>() { "n202400017", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("3"),
                    operationRequestID = new OperationRequestID("3"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400002","d202400003" } },
                        { "anaesthetist", new List<string>() { "d202400008","n202400011" } },
                        { "instrumenting", new List<string>() { "n202400014", } },
                        { "circulating", new List<string>() { "n202400017", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("4"),
                    operationRequestID = new OperationRequestID("4"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400002","d202400003" } },
                        { "anaesthetist", new List<string>() { "d202400008","n202400011" } },
                        { "instrumenting", new List<string>() { "n202400014", } },
                        { "circulating", new List<string>() { "n202400017", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("5"),
                    operationRequestID = new OperationRequestID("5"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400002","d202400003" } },
                        { "anaesthetist", new List<string>() { "d202400008","n202400011" } },
                        { "instrumenting", new List<string>() { "n202400014", } },
                        { "circulating", new List<string>() { "n202400017", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("6"),
                    operationRequestID = new OperationRequestID("6"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400001",
                    operationTypeID = "so2",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Effective,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400001","d202400002","d202400003" } },
                        { "anaesthetist", new List<string>() { "d202400008","n202400011" } },
                        { "instrumenting", new List<string>() { "n202400014", } },
                        { "circulating", new List<string>() { "n202400017", } },
                        { "medical_action", new List<string>() { "s202400001", } }
                    }
                },

                new OperationRequest
                {

                    Id = new OperationRequestID("7"),
                    operationRequestID = new OperationRequestID("7"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("8"),
                    operationRequestID = new OperationRequestID("8"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("9"),
                    operationRequestID = new OperationRequestID("9"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("10"),
                    operationRequestID = new OperationRequestID("10"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("11"),
                    operationRequestID = new OperationRequestID("11"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Effective,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("12"),
                    operationRequestID = new OperationRequestID("12"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Effective,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                new OperationRequest
                {

                    Id = new OperationRequestID("13"),
                    operationRequestID = new OperationRequestID("13"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400004",
                    operationTypeID = "so3",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400004","d202400005" } },
                        { "anaesthetist", new List<string>() { "d202400009","n202400012" } },
                        { "instrumenting", new List<string>() { "n202400015", } },
                        { "circulating", new List<string>() { "n202400018", } },
                        { "medical_action", new List<string>() { "s202400002", } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("14"),
                    operationRequestID = new OperationRequestID("14"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("19"),
                    operationRequestID = new OperationRequestID("19"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Emergency,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("20"),
                    operationRequestID = new OperationRequestID("20"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("15"),
                    operationRequestID = new OperationRequestID("15"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("16"),
                    operationRequestID = new OperationRequestID("16"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Urgent,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("17"),
                    operationRequestID = new OperationRequestID("17"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Effective,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                },
                 new OperationRequest
                {
                    Id = new OperationRequestID("18"),
                    operationRequestID = new OperationRequestID("18"),
                    patientID = ((Patient)GetRandomItem(patients)).Id.ToString(),
                    doctorID = "d202400006",
                    operationTypeID = "so4",
                    deadlineDate = new DeadlineDate(1, 2, 2025),
                    priority = Priority.Effective,
                    specializations = new Dictionary<string, List<string>>()
                    {
                        { "orthopaedist", new List<string>() { "d202400006","d202400007" } },
                        { "anaesthetist", new List<string>() { "n202400013", "d202400010" } },
                        { "instrumenting", new List<string>() { "n202400016" } },
                        { "circulating", new List<string>() { "n202400019" } },
                        { "medical_action", new List<string>() { "s202400003" } }
                    }
                }};
        
        foreach(var operationRequest in operationRequests)
        {
            var patient = patients[0];
            patients.RemoveAt(0);
            operationRequest.patientID = patient.Id.ToString();
        }
                
        modelBuilder.Entity<OperationRequest>().HasData( operationRequests
        );


        List<AvailabilitySlot> availabilitySlots = new List<AvailabilitySlot>();
        foreach(var staff in staffs)
        {
            availabilitySlots.Add(
                new AvailabilitySlot
                {
                    Id = staff.staffID,
                    StaffID = staff.staffID,
                    Slots = new Dictionary<int, Slot>()
                    {
                        { 20250101, new Slot(480, 1200) },
                        { 20250102, new Slot(480, 1200) },
                        { 20250103, new Slot(480, 1200) },
                        { 20250104, new Slot(480, 1200) },
                        { 20250105, new Slot(480, 1200) },
                        { 20250106, new Slot(480, 1200) },
                        { 20250107, new Slot(480, 1200) },
                        { 20250108, new Slot(480, 1200) },
                        { 20250109, new Slot(480, 1200) },
                        { 20250110, new Slot(480, 1200) },
                        { 20250111, new Slot(480, 1200) },
                        { 20250112, new Slot(480, 1200) },
                        { 20250113, new Slot(480, 1200) },
                        { 20250114, new Slot(480, 1200) },
                        { 20250115, new Slot(480, 1200) },
                        { 20250116, new Slot(480, 1200) },
                        { 20250117, new Slot(480, 1200) },
                        { 20250118, new Slot(480, 1200) },
                        { 20250119, new Slot(480, 1200) },
                        { 20250120, new Slot(480, 1200) },
                        { 20250121, new Slot(480, 1200) },
                        { 20250122, new Slot(480, 1200) },
                        { 20250123, new Slot(480, 1200) },
                        { 20250124, new Slot(480, 1200) },
                        { 20250125, new Slot(480, 1200) },
                        { 20250126, new Slot(480, 1200) },
                        { 20250127, new Slot(480, 1200) },
                        { 20250128, new Slot(480, 1200) },
                        { 20250129, new Slot(480, 1200) },
                        { 20250130, new Slot(480, 1200) },
                        { 20250131, new Slot(480, 1200) },
                    }
                }
            );
        }

        modelBuilder.Entity<AvailabilitySlot>().HasData(
            availabilitySlots     
        );

        modelBuilder.Entity<OperationType>().HasData(

            new OperationType
            {
                Id = new OperationTypeName("so2"),
                operationTypeName = new OperationTypeName("so2"),
                operationTypeDescription = new OperationTypeDescription("Knee Replacement Surgery"),
                estimatedDuration = new EstimatedDuration(10,30,10),
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
                estimatedDuration = new EstimatedDuration(15,20,15),
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
                estimatedDuration = new EstimatedDuration(10,35,5),
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
        public static T GetRandomItem<T>(List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("The list cannot be null or empty.", nameof(list));
        }

        var random = new Random();
        int index = random.Next(list.Count);
        return list[index];
    }
}
