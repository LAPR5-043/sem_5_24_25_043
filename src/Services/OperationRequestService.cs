using System;
using Domain.OperationRequestAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services
{
    public class OperationRequestService : IOperationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOperationRequestRepository operationRequestRepository;


        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository)
        {
            this.unitOfWork = unitOfWork;
            this.operationRequestRepository = operationRequestRepository;
        }
        
    }
}