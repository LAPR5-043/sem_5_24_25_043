
using System;
using System.Reflection;
using Domain.PatientAggregate;
using Domain.SpecializationAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services.Services
{
    public class PendingRequestService : IPendingRequestService
    {

        private readonly IPendingRequestRepository pendingRequestRepository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ILogService logService;
        public PendingRequestService(IUnitOfWork unitOfWork, IPendingRequestRepository pendingRequestRepository,  ILogService logService)
        {
            this.unitOfWork = unitOfWork;
            this.pendingRequestRepository = pendingRequestRepository;

            this.logService = logService;

            
        }

        public PendingRequest AddPendingRequest(string userID, string oldValue, string newValue, string type)
        {
            PendingRequest pendingRequest = new PendingRequest();
            pendingRequest.userId = userID;
            pendingRequest.oldValue = oldValue;
            pendingRequest.pendingValue = newValue;
            pendingRequest.attributeName = type;
            PendingRequest result = pendingRequestRepository.AddAsync(pendingRequest).Result;
            unitOfWork.CommitAsync();

            return result;
        }

        public PendingRequest GetByIdAsync(LongId id)
        {
            return pendingRequestRepository.GetByIdAsync(id).Result;
        }

    }
}    