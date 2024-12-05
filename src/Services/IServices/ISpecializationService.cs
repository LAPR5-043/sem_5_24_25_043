using System;

namespace src.Services.IServices
{
    public interface ISpecializationService
    {

        Task<List<SpecializationDto>> GetSpecializationsAsync();

        Task<SpecializationDto> GetSpecializationAsync(string specializationID);
        Task<SpecializationDto> CreateSpecializationAsync(SpecializationDto specializationDto);
        Task<SpecializationDto> UpdateSpecializationAsync(string id, SpecializationDto specializationDto);
    }
}