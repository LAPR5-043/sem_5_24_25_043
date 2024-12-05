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

        public async Task<SpecializationDto> CreateSpecializationAsync(SpecializationDto specializationDto){
            var specialization = new Specialization { specializationDescription = new SpecializationDescription(specializationDto.SpecializationDescription ),
                                                        specializationName = new SpecializationName(specializationDto.SpecializationName),
                                                        Id = new SpecializationName(specializationDto.SpecializationName) };
            try
            {
                var result = specializationRepository.AddAsync(specialization).Result;
                await unitOfWork.CommitAsync();
                return new SpecializationDto(result);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SpecializationDto> UpdateSpecializationAsync(string id, SpecializationDto specializationDto){
            var specialization = specializationRepository.GetByIdAsync(new SpecializationName(id)).Result;
    

            if (specializationDto.SpecializationDescription != null)
            {
                specialization.specializationDescription = new SpecializationDescription(specializationDto.SpecializationDescription);
            }
            try
            {
                var result = specializationRepository.UpdateAsync(specialization);
                await unitOfWork.CommitAsync();
                return new SpecializationDto(result);

            }
            catch (Exception e)
            {
                throw new Exception("Specialization Already Exists");
            }
        }
    }
}