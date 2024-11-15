using System;
using Domain.SpecializationAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services.Services
{
    public class SpecializationService : ISpecializationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISpecializationRepository specializationRepository;

        public SpecializationService(IUnitOfWork unitOfWork, ISpecializationRepository specializationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.specializationRepository = specializationRepository;
        }

        public async Task<List<SpecializationDto>> GetSpecializationsAsync(){
            List<SpecializationDto> specializationsDto = new List<SpecializationDto>();
            var specializations = await specializationRepository.GetAllAsync();
            foreach (var specialization in specializations)
            {
                specializationsDto.Add(new SpecializationDto(specialization));
            }

            return specializationsDto;
        }

        public Task<SpecializationDto> GetSpecializationAsync(string specializationID){
            return specializationRepository.GetByIdAsync(new SpecializationName(specializationID))
                .ContinueWith(specialization => new SpecializationDto(specialization.Result));

        }
    }
}