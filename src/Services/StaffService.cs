using Domain.StaffAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Domain.Shared;
using src.Services.IServices;
using AppContext = src.Models.AppContext;

namespace src.Controllers.Services;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IStaffRepository staffRepository;
    private readonly ILogService logService;
    

    private static string staffDeactivateLog1 = "Staff status changed with success";
    private static string staffDeactivateLog2 = ";StaffId:";
    private static string staffDeactivateLog3 = ";NewStatus:";

    public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository, ILogService logService)
    {
        this.unitOfWork = unitOfWork;
        this.staffRepository = staffRepository;
        this.logService = logService;
    }
    private Boolean ValidateStaffInformation(string email, string phoneNumber, string licenseNumber)
    {
        return staffRepository.StaffExists(email, phoneNumber, licenseNumber);
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
        if (staffDto.AvailabilitySlots != null) { staffToEdit.changeAvailabilitySlots(new AvailabilitySlots(TimeSlot.timeSlotsFromString(staffDto.AvailabilitySlots))); 
            logMessage += "Availability Slots: " + staffDto.AvailabilitySlots + "; "; }
        if (staffDto.SpecializationID != null) { staffToEdit.changeSpecializationID(staffDto.SpecializationID);
            logMessage += "Specialization: " + staffDto.SpecializationID + "; "; }
        
        await logService.CreateLogAsync(logMessage, adminEmail);
        staffRepository.UpdateAsync(staffToEdit);
        await unitOfWork.CommitAsync();

        return true;
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

        query = query.Where(s => s.isActive == true);

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
        var resultDtos = result.Select(s => new StaffDto(s)).ToList();

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

        if (ValidateStaffInformation(staffDto.Email, staffDto.PhoneNumber, staffDto.LicenseNumber))
        {
            throw new InvalidOperationException("Staff already exists.");
        }

        staffDto.AvailabilitySlots = staffDto.AvailabilitySlots ?? new List<string>();

        var newStaff = new Staff();

        newStaff.staffID = new StaffID(staffDto.StaffID);
        newStaff.firstName = new StaffFirstName(staffDto.FirstName);
        newStaff.lastName = new StaffLastName(staffDto.LastName);
        newStaff.fullName = new StaffFullName(newStaff.firstName, newStaff.lastName);
        newStaff.email = new StaffEmail(staffDto.Email);
        newStaff.phoneNumber = new StaffPhoneNumber(staffDto.PhoneNumber);
        newStaff.licenseNumber = new LicenseNumber(staffDto.LicenseNumber);
        newStaff.isActive = (bool)staffDto.IsActive;
        newStaff.availabilitySlots = new AvailabilitySlots(TimeSlot.timeSlotsFromString(staffDto.AvailabilitySlots));
        newStaff.specializationID = staffDto.SpecializationID;

        await staffRepository.AddAsync(newStaff);
        await unitOfWork.CommitAsync();

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

  

   
}