using System;
using Domain.AppointmentAggregate;
using src.Domain.Shared;
using src.Services.IServices;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;


namespace src.Services
{
    /// <summary>
    /// Appointment service
    /// </summary>
    public class AppointmentService : IAppointmentService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Appointment repository
        /// </summary>
        private readonly IAppointmentRepository appointmentRepository;

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="appointmentRepository"></param>
        public AppointmentService(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.appointmentRepository = appointmentRepository;
        }

        /*
        public async Task<string> GetIdFromRequestId(string requestID)
        {
            Appointment appointment = await appointmentRepository.GetAppointmentByRequestID(requestID);
            return appointment.appointmentID.ToString();
        }
        */
    }
}
