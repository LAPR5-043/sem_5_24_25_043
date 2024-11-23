using System;
using Microsoft.AspNetCore.Mvc;
using Schedule;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;

namespace src.Services.IServices
{
    public interface IAppointmentService
    {
        List<AppointmentDto> GetDayAppointmentsAsync(int day);
        Task<PlanningResponseDto> GenerateApointmentsByRoomAndDateAsync(String RoomId, int date);
        List<AppointmentDto> GetAllAppointmentsAsync();
    }
}