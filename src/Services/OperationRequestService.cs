using System;
using System.Diagnostics;
using Domain.AppointmentAggregate;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain.Shared;
using src.Services.IServices;
using System.Text.RegularExpressions;


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
        /// Create a new operation request
        /// </summary>
        /// <param name="operationRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> CreateOperationRequestAsync(OperationRequestDto operationRequestDto, string email)
        {

            if (operationRequestDto == null)
            {
                throw new ArgumentNullException(nameof(operationRequestDto), "Operation Request data is null.");
            }

            var newOperationRequest = new OperationRequest();

            string doctorID = await staffService.GetIdFromEmailAsync(email);

            StaffDto staffDto = await staffService.GetStaffAsync(doctorID);


            VerifyPatientID(operationRequestDto.PatientID);
            newOperationRequest.patientID = operationRequestDto.PatientID;

            if (operationRequestDto.OperationTypeID != staffDto.SpecializationID)
            {
                throw new ArgumentException("Operation Type must match the specialization of the doctor");
            }
            newOperationRequest.operationTypeID = operationRequestDto.OperationTypeID;

            newOperationRequest.doctorID = staffDto.StaffID;

            newOperationRequest.priority = PriorityExtensions.FromString(operationRequestDto.Priority);

            int day;
            if (!int.TryParse(operationRequestDto.Day, out day))
            {
                throw new ArgumentException("Day must be a number");
            }

            int month;
            if (!int.TryParse(operationRequestDto.Month, out month))
            {
                throw new ArgumentException("Month must be a number");
            }

            int year;
            if (!int.TryParse(operationRequestDto.Year, out year))
            {
                throw new ArgumentException("Year must be a number");

            }

            newOperationRequest.deadlineDate = new DeadlineDate(day, month, year);

            await operationRequestRepository.AddAsync(newOperationRequest);
            await unitOfWork.CommitAsync();

            return newOperationRequest != null;
        }

        /// <summary>
        /// Get operation requests filtered by different criteria
        /// </summary>
        /// <param name="firstName">First name of the patient</param>
        /// <param name="lastName">Last name of the patient</param>
        /// <param name="operationType">Type of operation</param>
        /// <param name="priority">Priority of the operation</param>
        /// <param name="status">Status of the operation</param>
        /// <param name="sortBy">Column to sort by</param>
        /// <returns>List of operation requests</returns>
        public Task<ActionResult<IEnumerable<OperationRequestDto>>> GetOperationRequestFilteredAsync(string? firstName, string? lastName, string? operationType, string? priority, string? status, string? sortBy)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetOperationRequestByIdAsync(string id)
        {
            var operationRequest = operationRequestRepository.GetByIdAsync(new OperationRequestID(id));
            if (operationRequest == null)
            {
                throw new Exception("Operation Request not found.");
            }

            //return Task.FromResult(new OperationRequestDto(operationRequest.Result));
            throw new NotImplementedException();
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
            
            if (operationRequest == null)
            {
                throw new Exception("Operation Request not found");
            }


            await checkIfDoctorIsTheCreatorOfOperationRequestAsync(id, doctorID, operationRequest);

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

        public async Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto, string email)
        {

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
            try
            {
                if (!operationRequestDto.PatientID.IsNullOrEmpty())
                {
                    operationRequest.patientID = operationRequestDto.PatientID;
                    await logService.CreateLogAsync("Update Operation Request" + "Patient ID changed from " + operationRequest.patientID + " to " + operationRequestDto.PatientID, email);
                }
                if (!operationRequestDto.DoctorID.IsNullOrEmpty())
                {
                    operationRequest.doctorID = operationRequestDto.DoctorID;
                    await logService.CreateLogAsync("Update Operation Request" + "Doctor ID changed from " + operationRequest.doctorID + " to " + operationRequestDto.DoctorID, email);
                }
                if (!operationRequestDto.OperationTypeID.IsNullOrEmpty())
                {
                    operationRequest.operationTypeID = operationRequestDto.OperationTypeID;
                    await logService.CreateLogAsync("Update Operation Request" + "Operation Type changed from " + operationRequest.operationTypeID + " to " + operationRequestDto.OperationTypeID, email);
                }
                if (operationRequestDto.Day != null && operationRequestDto.Month != null && operationRequestDto.Year != null)
                {
                    operationRequest.deadlineDate = new DeadlineDate(int.Parse(operationRequestDto.Day), int.Parse(operationRequestDto.Month), int.Parse(operationRequestDto.Year));
                    await logService.CreateLogAsync("Update Operation Request" + "Deadline Date changed from " + operationRequest.deadlineDate + " to " + operationRequestDto.Day + "/" + operationRequestDto.Month + "/" + operationRequestDto.Year, email);
                }
                if (!operationRequestDto.Priority.IsNullOrEmpty())
                {
                    operationRequest.priority = PriorityExtensions.FromString(operationRequestDto.Priority);
                    await logService.CreateLogAsync("Update Operation Request" + "Priority changed from " + operationRequest.priority + " to " + operationRequestDto.Priority, email);
                }
                await operationRequestRepository.updateAsync(operationRequest);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;

        }

        private void VerifyPatientID(string patientID)
        {
            if (!Regex.IsMatch(patientID, @"^$|^\d+$"))
            {
                throw new ArgumentException("The Patient's ID must be a number");
            }
        }
    }
}