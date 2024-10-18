using Domain.StaffAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Domain.Shared;
using AppContext= src.Models.AppContext;

namespace src.Controllers.Services;

public class StaffService 
{
    private  readonly IUnitOfWork unitOfWork;
    private readonly IStaffRepository staffRepository;

    public StaffService(IUnitOfWork unitOfWork, IStaffRepository staffRepository)
    {
        this.unitOfWork = unitOfWork;
        this.staffRepository = staffRepository;
    }

    public async Task<OkObjectResult> getAllStaffAsync()
    {
        var result = await staffRepository.GetAllAsync();
        IEnumerable<StaffDto> resultDtos =  new List<StaffDto>();
        foreach (var staff in result)
        {
            resultDtos.Append(new StaffDto(staff));
        }
        return new OkObjectResult(resultDtos);
    }

    public async Task<ActionResult<IEnumerable<StaffDto>>> getStaffsFilteredAsync(string? firstName, string? lastName, string? email, string? specialization, string? sortBy )
    {   
        bool ascending = true;
        var result = await staffRepository.GetAllAsync();
        IEnumerable<StaffDto> resultDtos =  new List<StaffDto>();
        foreach (var staff in result)
        {
            resultDtos.Append(new StaffDto(staff));
        }

        if (firstName != null)
        {
            resultDtos = (List<StaffDto>)resultDtos.Where(s => s.FirstName == firstName);
        }
        if (lastName != null)
        {
            resultDtos = (List<StaffDto>)resultDtos.Where(s => s.LastName == lastName);
        }
        if (email != null)
        {
            resultDtos = (List<StaffDto>)resultDtos.Where(s => s.Email == email);
        }
        if (specialization != null)
        {
            resultDtos = (List<StaffDto>)resultDtos.Where(s => s.SpecializationID == specialization);
        }

        if (!string.IsNullOrEmpty(sortBy))
        {
            switch (sortBy.ToLower())
            {
                case "firstname":
                    resultDtos = ascending ? resultDtos.OrderBy(s => s.FirstName) : resultDtos.OrderByDescending(s => s.FirstName);
                    break;
                case "lastname":
                    resultDtos = ascending ? resultDtos.OrderBy(s => s.LastName) : resultDtos.OrderByDescending(s => s.LastName);
                    break;
                case "email":
                    resultDtos = ascending ? resultDtos.OrderBy(s => s.Email) : resultDtos.OrderByDescending(s => s.Email);
                    break;
                case "specialization":
                    resultDtos = ascending ? resultDtos.OrderBy(s => s.SpecializationID) : resultDtos.OrderByDescending(s => s.SpecializationID);
                    break;
                default:
                    break;
            }
        }

        return new ActionResult<IEnumerable<StaffDto>>(resultDtos);
    }

    internal async Task<StaffDto> getStaffAsync(string id)
    {
        var staff = await staffRepository.GetByIdAsync(new StaffID(id));
        return new StaffDto(staff);
    }
}