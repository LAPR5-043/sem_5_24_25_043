using System;
using Domain.AppointmentAggregate;
using Domain.OperationRequestAggregate;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services
{
    public class OperationRequestService : IOperationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOperationRequestRepository operationRequestRepository;
        //private readonly IAppointmentRepository appointmentRepository;


        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository)
        {
            this.unitOfWork = unitOfWork;
            this.operationRequestRepository = operationRequestRepository;
            //this.appointmentRepository = appointmentRepository;
        }

        public async Task<bool> DeleteOperationRequestAsync(int id)
        {

            var operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id.ToString()));
            
            if (operationRequest == null)
            {
                return false;
            }

            /*var result = await EnsureOperationIsNotScheduledAsync(id);

            if (!result)
            {
                return false;
            }*/

            operationRequestRepository.Remove(operationRequest);
            await unitOfWork.CommitAsync();

            // TODO Planning Module

            return true;

        }

        /*private async Task<bool> EnsureOperationIsNotScheduledAsync(int id)
        {
            var appointment = await appointmentRepository.CheckIfOperationIsScheduled(id);

            if (appointment)
            {
                return false;
            }

            return true;
            
        }*/
    }
}