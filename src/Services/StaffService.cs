using Domain.StaffAggregate;
using Microsoft.AspNetCore.Mvc;
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
    private Boolean validateStaffInformation(string email, string phoneNumber)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
        {
            throw new Exception("Invalid patient data.");
        }

        if (!email.Contains("@"))
        {
            throw new Exception("Invalid email.");
        }

        if (phoneNumber.Length != 9)
        {
            throw new Exception("Invalid phone number.");
        }

        return staffRepository.StaffExists(email, phoneNumber);
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

    public async Task<ActionResult<IEnumerable<StaffDto>>> getStaffsFilteredAsync(string? firstName, string? lastName, string? email, string? specialization, string? sortBy)
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
            query = query.Where(s => s.specializationID == specialization);
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

    public async Task<StaffDto> CreateStaffAsync(StaffDto staffDto)
    {
        if (staffDto == null)
        {
            throw new ArgumentNullException(nameof(staffDto), "Patient data is null.");
        }

        if (!validateStaffInformation(staffDto.Email, staffDto.PhoneNumber))
        {
            throw new Exception("Staff already exists.");
        }

        staffDto.AvailabilitySlots = staffDto.AvailabilitySlots ?? new List<string>();

        var staff = new Staff();
        staff.firstName = new StaffFirstName(staffDto.FirstName);
        staff.lastName = new StaffLastName(staffDto.LastName);
        staff.fullName = new StaffFullName(staff.firstName, staff.lastName);
        staff.email = new StaffEmail(staffDto.Email);
        staff.phoneNumber = new StaffPhoneNumber(staffDto.PhoneNumber);
        staff.licenseNumber = new LicenseNumber(staffDto.LicenseNumber);
        staff.isActive = (bool)staffDto.IsActive;
        staff.availabilitySlots = new AvailabilitySlots(TimeSlot.timeSlotsFromString(staffDto.AvailabilitySlots));
        staff.specializationID = staffDto.SpecializationID;

        await staffRepository.AddAsync(staff);
        await unitOfWork.CommitAsync();

        return new StaffDto(staff);
    }

    public async Task<bool> UpdateIsActiveAsync(string id)
    {
        var staff = await staffRepository.GetByIdAsync(new StaffID(id));
        if (staff == null)
        {
            return false;
        }
        staff.isActive = !staff.isActive;
        staffRepository.UpdateAsync(staff);
        await unitOfWork.CommitAsync();
        await logService.CreateLogAsync(staffDeactivateLog1 + staffDeactivateLog2 + staff.staffID + staffDeactivateLog3 + staff.isActive, "colocar@emailtoken.aqui");
        return true;
    }

    public async Task<string> GetIdFromEmailAsync(string doctorEmail)
    {
        Staff staff = await staffRepository.GetStaffByEmail(doctorEmail);
        return staff.staffID.ToString();
    }
}