using System;
using Domain.AppointmentAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.appointmentRepository = appointmentRepository;
        }
    }
}