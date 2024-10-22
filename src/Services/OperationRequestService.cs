using System;
using System.Diagnostics;
using Domain.AppointmentAggregate;
using Domain.OperationRequestAggregate;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services
{
    public class OperationRequestService : IOperationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOperationRequestRepository operationRequestRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPlanningModuleService planningModuleService;


        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository, IAppointmentRepository appointmentRepository, IPlanningModuleService planningModuleService)
        {
            this.unitOfWork = unitOfWork;
            this.operationRequestRepository = operationRequestRepository;
            this.appointmentRepository = appointmentRepository;
            this.planningModuleService = planningModuleService;
        }
  
        public async Task<bool> DeleteOperationRequestAsync(int id)
        {

            var operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id.ToString()));
            
            if (operationRequest == null)
            {
                throw new Exception("Operation Request not found");
            }

            var result = await appointmentRepository.CheckIfOperationIsScheduled(id);

            Console.WriteLine("Result: " + result);

            if (result)
            {
                throw new Exception("Operation Request is already scheduled");
            }

            operationRequestRepository.Remove(operationRequest);
            await unitOfWork.CommitAsync();

            // TODO Planning Module

            planningModuleService.NotifyOperationRequestDeleted(id);

            return true;

        }

        public async Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto){
            
            OperationRequest operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id.ToString()));

            if (operationRequest == null)
            {
                return false;
            }
            
            try{
                if(operationRequestDto.patientID != null ){
                    operationRequest.patientID = (int)operationRequestDto.patientID;     
                }
                if(operationRequestDto.doctorID != null || operationRequestDto.doctorID != ""){
                    operationRequest.doctorID = operationRequestDto.doctorID;
                }
                if(operationRequestDto.operationType != null || operationRequestDto.operationType != ""){
                    operationRequest.operationTypeID = operationRequestDto.operationType;
                }
                if( operationRequestDto.day != null || operationRequestDto.month != null || operationRequestDto.year != null){
                    operationRequest.deadlineDate = new DeadlineDate((int)operationRequestDto.day, (int)operationRequestDto.month, (int)operationRequestDto.year);
                }
                if(operationRequestDto.priority != null || operationRequestDto.priority != ""){
                    operationRequest.priority = PriorityExtensions.FromString(operationRequestDto.priority);
                }
                await operationRequestRepository.updateAsync(operationRequest);
            } catch (Exception e) {
                Debug.WriteLine(e.StackTrace);
                return false;
            }

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