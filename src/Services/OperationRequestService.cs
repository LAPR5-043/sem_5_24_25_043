using System;
using Domain.AppointmentAggregate;
using Domain.OperationRequestAggregate;
using Microsoft.AspNetCore.Mvc;
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
        //private readonly IAppointmentRepository appointmentRepository;


        public OperationRequestService(IUnitOfWork unitOfWork, IOperationRequestRepository operationRequestRepository)
        {
            this.unitOfWork = unitOfWork;
            this.operationRequestRepository = operationRequestRepository;
            //this.appointmentRepository = appointmentRepository;
        }

        public async Task<OkObjectResult> getAllOperationRequestsAsync()
        {
            var result = await operationRequestRepository.GetAllAsync();
            IEnumerable<OperationRequestDto> resultDtos = new List<OperationRequestDto>();
            foreach (var operationRequest in result)
            {
                resultDtos.Append(new OperationRequestDto(operationRequest));
            }
            return new OkObjectResult(resultDtos);

        }

        /*
        public async Task<ActionResult<IEnumerable<OperationRequestDto>>> getOperationRequestsFilteredAsync(string? firstName, string? lastName, string? operationType, string? priority, string? sortBy)
        {
            bool ascending = true;
            var operationRequestList = await operationRequestRepository.GetAllAsync();
            var query = operationRequestList.AsQueryable();

            
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(s => s.firstName.firstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(s => s.lastName.lastName.Contains(lastName));
            }


            if (!string.IsNullOrEmpty(operationType))
            {
                query = query.Where(s => s.operationTypeID == operationType);
            }

            // MIGHT NOT WORK - TO CHECK!
            if (!string.IsNullOrEmpty(priority) && Enum.TryParse(priority, true, out Priority priorityEnumValue))
            {
                query = query.Where(o => o.priority == priorityEnumValue);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "operationType":
                        query = query.OrderBy(o => o.operationTypeID);
                        break;
                    case "lastname":
                        query = query.OrderBy(s => s.lastName.lastName);
                        break;
                    case "email":
                        query = query.OrderBy(s => s.email.email);
                        break;
                    default:
                        query = query.OrderBy(s => s.firstName.firstName);
                        break;
                }
            }
            

            var result = query.ToList();
            var resultDtos = result.Select(o => new OperationRequestDto(o)).ToList();

            return resultDtos;
        }
        */

        public async Task<OperationRequestDto> getOperationRequestAsync(string id)
        {
            var operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id));
            return new OperationRequestDto(operationRequest);
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

        public async Task<bool> UpdateOperationRequestAsync(int id, OperationRequestDto operationRequestDto)
        {

            var operationRequest = await operationRequestRepository.GetByIdAsync(new OperationRequestID(id.ToString()));

            if (operationRequest == null)
            {
                return false;
            }

            try
            {
                /*
                operationRequest.patientID = operationRequestDto.patientID;
                operationRequest.doctorID = operationRequestDto.doctorID;
                operationRequest.operationTypeID = operationRequestDto.operationType;
                operationRequest.deadlineDate = new DeadlineDate(operationRequestDto.day, operationRequestDto.month, operationRequestDto.year);
                operationRequest.priority = PriorityExtensions.FromString(operationRequestDto.priority);
                await operationRequestRepository.updateAsync(operationRequest);
                */
            }
            catch (Exception e)
            {
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