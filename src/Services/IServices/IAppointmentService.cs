using System;
using Schedule;
using sem_5_24_25_043.Domain.AppointmentAggregate;

namespace src.Services.IServices
{
    public interface IAppointmentService
    {
        ScheduleDto GenerateApointmentsByRoomAndDate(String RoomId, int date);
    }
}