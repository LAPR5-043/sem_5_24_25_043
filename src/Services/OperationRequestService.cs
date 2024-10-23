using System;
using System.Diagnostics;
using Domain.AppointmentAggregate;
using Domain.OperationRequestAggregate;
using Microsoft.IdentityModel.Tokens;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services
{
    /// <summary>
    /// Operation Request Service
    /// </summary>
    public class OperationRequestService : IOperationRequestService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        /// <summary>
        /// Operation Request Repository
        /// </summary>
        private readonly IOperationRequestRepository operationRequestRepository;
        /// <summary>
        /// Appointment Repository
        /// </summary>
        private readonly IAppointmentRepository appointmentRepository;
        /// <summary>
        /// Planning Module Service
        /// </summary>
        private readonly IPlanningModuleService planningModuleService;

        private readonly IStaffService staffService;

        private readonly ILogService logService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="operationRequestRepository"></param>
        /// <param name="appointmentRepository"></param>
        /// <param name="planningModuleService"></param>
        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository, IAppointmentRepository appointmentRepository, IPlanningModuleService planningModuleService, IStaffService staffService, ILogService logService)
        {
            this.unitOfWork = unitOfWork;
            this.operationRequestRepository = operationRequestRepository;
            this.appointmentRepository = appointmentRepository;
            this.planningModuleService = planningModuleService;
            this.staffService = staffService;
            this.logService = logService;
        }
        /// <summary>
        /// Delete operation request by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteOperationRequestAsync(int id, string doctorEmail)
        {

            // Check if the doctor is the one that created the Operation Request
            var doctorID = await staffService.GetIdFromEmailAsync(doctorEmail);

            OperationRequest operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id.ToString()));

            await checkIfDoctorIsTheCreatorOfOperationRequestAsync(id, doctorID, operationRequest);
            
            if (operationRequest == null)
            {
                throw new Exception("Operation Request not found");
            }

            var result = await appointmentRepository.CheckIfOperationIsScheduled(id);

            //Console.WriteLine("Result: " + result);

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

        private async Task checkIfDoctorIsTheCreatorOfOperationRequestAsync(int id, string doctorID, OperationRequest operationRequest)
        {

            if (operationRequest.doctorID != doctorID)
            {
                throw new Exception("Doctor is not the creator of the Operation Request");
            }
        }

        public async Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto, string email){
            
            OperationRequest operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id.ToString()));

           // string doctor = await staffService.GetIdFromEmailAsync(email);

           /* if (!doctor.Equals(operationRequest.doctorID) == false)
            {
                return false;
            }

            if (operationRequest == null)
            {
                return false;
            }*/
            
            try{
                if(operationRequestDto.patientID != null ){
                    operationRequest.patientID = (int)operationRequestDto.patientID; 
                    await logService.CreateLogAsync("Update Operation Request"+ "Patient ID changed from " + operationRequest.patientID + " to " + operationRequestDto.patientID, email);    
                }
                if(!operationRequestDto.doctorID.IsNullOrEmpty()){
                    operationRequest.doctorID = operationRequestDto.doctorID;
                    await logService.CreateLogAsync("Update Operation Request"+ "Doctor ID changed from " + operationRequest.doctorID + " to " + operationRequestDto.doctorID, email);
                }
                if(!operationRequestDto.operationType.IsNullOrEmpty()){
                    operationRequest.operationTypeID = operationRequestDto.operationType;
                    await logService.CreateLogAsync("Update Operation Request"+ "Operation Type changed from " + operationRequest.operationTypeID + " to " + operationRequestDto.operationType, email);
                }
                if( operationRequestDto.day != null  && operationRequestDto.month != null  && operationRequestDto.year != null ){
                    operationRequest.deadlineDate = new DeadlineDate((int)operationRequestDto.day, (int)operationRequestDto.month, (int)operationRequestDto.year);
                    await logService.CreateLogAsync("Update Operation Request"+ "Deadline Date changed from " + operationRequest.deadlineDate + " to " + operationRequestDto.day + "/" + operationRequestDto.month + "/" + operationRequestDto.year, email);
                }
                if(!operationRequestDto.priority.IsNullOrEmpty() ){
                    operationRequest.priority = PriorityExtensions.FromString(operationRequestDto.priority);
                    await logService.CreateLogAsync("Update Operation Request"+ "Priority changed from " + operationRequest.priority + " to " + operationRequestDto.priority, email);
                }
                await operationRequestRepository.updateAsync(operationRequest);
            } catch (Exception e) {
                Debug.WriteLine(e.StackTrace);
                return false;
            }

            return true;

        }
    }
}