using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using sem_5_24_25_043.Models;
using src.Infrastructure.OperationTypes;
using AppContext= src.Models.AppContext;

namespace src.Controllers.Services;

public class ManageStaffService 
{
    private StaffRepository staffRepository = Repositories.GetInstance().getStaffRepository();

    public ManageStaffService()
    {
        
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
            resultDtos = (List<StaffDto>)resultDtos.Where(s => s.email == email);
        }
        if (specialization != null)
        {
            resultDtos = (List<StaffDto>)resultDtos.Where(s => s.SpecializationName == specialization);
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
                    resultDtos = ascending ? resultDtos.OrderBy(s => s.email) : resultDtos.OrderByDescending(s => s.email);
                    break;
                case "specialization":
                    resultDtos = ascending ? resultDtos.OrderBy(s => s.SpecializationName) : resultDtos.OrderByDescending(s => s.SpecializationName);
                    break;
                default:
                    break;
            }
        }

        return new ActionResult<IEnumerable<StaffDto>>(resultDtos);
    }

    internal async Task<StaffDto> getStaffAsync(string id)
    {
        var staff = await staffRepository.GetByIdAsync(id);
        return new StaffDto(staff);
    }
}