using Domain.StaffAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sem_5_24_25_043;
using src.Domain.AvailabilitySlotAggregate;
using src.Domain.Shared;
using src.Services.IServices;
using AppContext = src.Models.AppContext;

namespace src.Controllers.Services;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IStaffRepository staffRepository;
    private readonly ILogService logService;
    private readonly IAuthService authService;
    private readonly IEmailService emailService;
    private readonly IAvailabilitySlotService availabilitySlotService;

    private static string staffDeactivateLog1 = "Staff status changed with success";
    private static string staffDeactivateLog2 = ";StaffId:";
    private static string staffDeactivateLog3 = ";NewStatus:";
    private static string medic = "rol_ANxHawgjqkHc7Rgr";
    private static string nurse = "rol_KvdAp8kpRA3FT57a";
    private static string tecnician = "rol_XkxIrMhp3RLpHna0";
    private static string admin = "rol_50uF6ByTAWQ9iPmF";

    public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, ILogService logService, IAuthService authService, IEmailService emailService, IAvailabilitySlotService availabilitySlotService)
    {
        this.unitOfWork = unitOfWork;
        this.staffRepository = staffRepository;
        this.logService = logService;
        this.authService = authService;
        this.emailService = emailService;
        this.availabilitySlotService = availabilitySlotService;
        this.emailService = emailService;
    }
    private Task<bool> ValidateStaffInformation(string email, string phoneNumber, string licenseNumber)
    {
        return Task.FromResult(staffRepository.StaffExists(email, phoneNumber, licenseNumber));
    }
    public async Task<bool> EditStaffAsync(string id, StaffDto staffDto, string adminEmail)
    {
        var staffToEdit = await staffRepository.GetByIdAsync(new StaffID(id));
        if (staffToEdit == null)
        {
            throw new Exception("Staff not found.");
        }
        
        if (staffDto.FirstName != null || staffDto.LastName != null || staffDto.LicenseNumber != null || staffDto.IsActive != null)
        {
            throw new InvalidOperationException("Attempt to change non-editable fields.");
        }
        string logMessage = "The staff properties: ";
        
        if (staffDto.Email != null) { staffToEdit.changeEmail(staffDto.Email);
            logMessage += "Email: " + staffDto.Email + "; "; }
        if (staffDto.PhoneNumber != null) { staffToEdit.changePhoneNumber(staffDto.PhoneNumber);
            logMessage += "Phone Number: " + staffDto.PhoneNumber + "; "; }
        if (staffDto.AvailabilitySlots != null) { changeAvailabilitySlots(staffToEdit, staffDto); 
            logMessage += "Availability Slots: " + staffDto.AvailabilitySlots + "; "; }
        if (staffDto.SpecializationID != null) { staffToEdit.changeSpecializationID(staffDto.SpecializationID);
            logMessage += "Specialization: " + staffDto.SpecializationID + "; "; }
        
        await logService.CreateLogAsync(logMessage, adminEmail);
        staffRepository.UpdateAsync(staffToEdit);
        await unitOfWork.CommitAsync();

        return true;
    }

    private void changeAvailabilitySlots(Staff staff ,StaffDto staffDto)
    {
        AvailabilitySlot av =availabilitySlotService.GetAvailabilitySlotAsync(staff.staffID.AsString()).Result;
        foreach (var slot in staffDto.AvailabilitySlots)
        {
            string[] slots = slot.Split(",");
            av.addAvailabilitySlot(int.Parse(slots[0]), int.Parse(slots[1]), int.Parse(slots[2]));
        }
        availabilitySlotService.UpdateAvailabilitySlot(av);
    }

    public async Task<OkObjectResult> getAllStaffAsync()
    {
        var result = await staffRepository.GetAllAsync();
        IEnumerable<StaffDto> resultDtos = new List<StaffDto>();
        foreach (var staff in result)
        {
            resultDtos.Append(new StaffDto(staff));
        }
        return new OkObjectResult(resultDtos);
    }

    public async Task<List<StaffDto>> getStaffsFilteredAsync(string? firstName, string? lastName, string? email, string? specialization, string? sortBy)
    {
        bool ascending = true;
        var staffList = await staffRepository.GetAllAsync();
        var query = staffList.AsQueryable();

        if (!string.IsNullOrEmpty(firstName))
        {
            query = query.Where(s => s.firstName.firstName.Contains(firstName));
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            query = query.Where(s => s.lastName.lastName.Contains(lastName));
        }

        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(s => s.email.email.Contains(email));
        }

        if (!string.IsNullOrEmpty(specialization))
        {
            query = query.Where(s => s.specializationID.Contains(specialization));
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "firstname":
                    query = query.OrderBy(s => s.firstName.firstName);
                    break;
                case "lastname":
                    query = query.OrderBy(s => s.lastName.lastName);
                    break;
                case "email":
                    query = query.OrderBy(s => s.email.email);
                    break;
                case "specialization":
                    query = query.OrderBy(s => s.specializationID);
                    break;
                default:
                    query = query.OrderBy(s => s.firstName.firstName);
                    break;
            }
        }

        var result = query.ToList();

        var resultDtos = new List<StaffDto>();
        
        foreach (var staff in result)
        {
            StaffDto staffDto = new StaffDto(staff);
            AvailabilitySlot av = availabilitySlotService.GetAvailabilitySlotAsync(staff.staffID.AsString()).Result;
            staffDto.AvailabilitySlots = staffDto.generateAvailabilitySlots(av);
            resultDtos.Add(staffDto);

        }

        return resultDtos;
    }

    public async Task<StaffDto> GetStaffAsync(string id)
    {
        var staff = await staffRepository.GetByIdAsync(new StaffID(id));
        return new StaffDto(staff);
    }

    public async Task<bool> CreateStaffAsync(StaffDto staffDto)
    {
        if (staffDto == null)
        {
            throw new ArgumentNullException(nameof(staffDto), "Staff data is null.");
        }

        if (await ValidateStaffInformation(staffDto.Email, staffDto.PhoneNumber, staffDto.LicenseNumber))
        {
            throw new InvalidOperationException("Staff already exists.");
        }

        staffDto.AvailabilitySlots = staffDto.AvailabilitySlots ?? new List<string>();

        var newStaff = new Staff();

      //  newStaff.staffID = new StaffID(staffDto.StaffID);
        newStaff.firstName = new StaffFirstName(staffDto.FirstName);
        newStaff.lastName = new StaffLastName(staffDto.LastName);
        newStaff.fullName = new StaffFullName(newStaff.firstName, newStaff.lastName);
        newStaff.email = new StaffEmail(staffDto.Email);
        newStaff.phoneNumber = new StaffPhoneNumber(staffDto.PhoneNumber);
        newStaff.licenseNumber = new LicenseNumber(staffDto.LicenseNumber);
        newStaff.isActive = (bool)staffDto.IsActive;
     
        newStaff.specializationID = staffDto.SpecializationID;
        await staffRepository.AddAsync(newStaff);
        await unitOfWork.CommitAsync();


        Staff nStaff = newStaff;

         availabilitySlotService.CreateAvailabilitySlotAsync(nStaff.staffID, staffDto.AvailabilitySlots).Wait();

        

        return newStaff != null;
    }


    public async Task<bool> UpdateIsActiveAsync(string id, String adminEmail)
    {
        var staff = await staffRepository.GetByIdAsync(new StaffID(id));
        if (staff == null)
        {
            return false;
        }
        staff.isActive = !staff.isActive;
        staffRepository.UpdateAsync(staff);
        await unitOfWork.CommitAsync();
        await logService.CreateLogAsync(staffDeactivateLog1 + staffDeactivateLog2 + staff.staffID + staffDeactivateLog3 + staff.isActive, adminEmail);
        return true;
    }
    
    public async Task<string> GetIdFromEmailAsync(string doctorEmail)
    {
        Staff staff = await staffRepository.GetStaffByEmail(doctorEmail);
        return staff.staffID.ToString();
    }
    public async Task<bool> signUpStaffAsync(string staffID,string iamEmail)
    {
        var staff =  await staffRepository.GetStaffByID(staffID);
        string role;
        switch (staffID.ToString().First())
        {
            case 'D':
            case 'd':
                role = medic;
                break;
            case 'N':
            case 'n':
                role = nurse;
                break;
            case 'T':
            case 't':
                role = tecnician;
                break;
            default:
                throw new InvalidOperationException("Invalid staff ID prefix.");
        }
        if (staff == null)
        {
            throw new InvalidOperationException("staff not found");
        }
        string password = GenerateRandomPassword(); 
        try
        {
            await authService.RegisterNewStaffAsync(iamEmail,staff.email.email,password,staff.fullName.fullName,role,staff.phoneNumber.phoneNumber);
            await emailService.SendEmailToStaffSignIn(iamEmail,password);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return true;
    }
    private string GenerateRandomPassword(int length = 12)
    {
        const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
        const string numericChars = "1234567890";
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            var byteBuffer = new byte[length];
            rng.GetBytes(byteBuffer);
            var charBuffer = new char[length];
            bool hasNumeric = false;

            for (int i = 0; i < length; i++)
            {
                charBuffer[i] = validChars[byteBuffer[i] % validChars.Length];
                if (numericChars.Contains(charBuffer[i]))
                {
                    hasNumeric = true;
                }
            }

            if (!hasNumeric)
            {
                charBuffer[length - 1] = numericChars[byteBuffer[length - 1] % numericChars.Length];
            }

            return new string(charBuffer);
        }
    }
    
}