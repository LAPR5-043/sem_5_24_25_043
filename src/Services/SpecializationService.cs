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
    }
}