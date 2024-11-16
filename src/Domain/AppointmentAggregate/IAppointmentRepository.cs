using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;
using src.Domain.Shared;

namespace Domain.AppointmentAggregate
{
    public interface IAppointmentRepository : IRepository<Appointment, AppointmentID>
    {
        Task<bool> CheckIfOperationIsScheduled(int requestID);
        Appointment GetOperationRequestByRequestId(int operationRequest);
    }
}