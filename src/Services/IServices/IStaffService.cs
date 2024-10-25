using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Domain.Shared;

namespace src.Services.IServices
{
    public interface IStaffService
    {
        Task<OkObjectResult> getAllStaffAsync();
        Task<List<StaffDto>> getStaffsFilteredAsync(string? firstName, string? lastName, string? email, string? specialization, string? sortBy);
        Task<bool> CreateStaffAsync(StaffDto staffDto);
        Task<StaffDto> GetStaffAsync(string id);
        Task<bool> UpdateIsActiveAsync(string id, string adminEmail);
        Task<string> GetIdFromEmailAsync(string doctorEmail);
        Task<bool> EditStaffAsync(string id, StaffDto staffDto, string adminEmail);
    }
}