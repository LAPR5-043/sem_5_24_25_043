using Domain.AppointmentAggregate;
using Microsoft.EntityFrameworkCore;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;
using src.Infrastructure.Shared;
using AppContext = src.Models.AppContext;

/// <summary>
/// Repository for appointments
/// </summary>
public class AppointmentRepository : BaseRepository<Appointment, AppointmentID>, IAppointmentRepository
{
    /// <summary>
    /// App context
    /// </summary>
    private readonly AppContext context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public AppointmentRepository(AppContext context) : base(context.Appointments)
    {
        this.context = context;
    }

    public Appointment GetOperationRequestByRequestId(int operationRequest)
    {
        return context.Appointments
            .AsEnumerable()
            .FirstOrDefault(a => a.requestID == operationRequest)!;
    }

    /// <summary>
    /// Check if operation is scheduled
    /// </summary>
    /// <param name="requestID"></param>
    /// <returns></returns>
    public async Task<bool> CheckIfOperationIsScheduled(int requestID)
    {
        return await context.Appointments.AnyAsync(a => a.requestID == requestID);
    }

    /*
    public async Task<Appointment> GetAppointmentByRequestID(string requestID)
    {
        return context.Appointments
            .AsEnumerable()
            .FirstOrDefault(a => a.requestID == int.Parse(requestID))!;
    }
    */

    public async Task<IEnumerable<Appointment>> GetDayAppointmentsAsync(int day)
    {
        return  context.Appointments
                .AsEnumerable()
            .Where(a => a.dateAndTime.date == day.ToString());
            
            
    }
}