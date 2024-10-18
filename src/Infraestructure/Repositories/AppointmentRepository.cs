using Domain.AppointmentAggregate;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;
using src.Infrastructure.Shared;
using AppContext = src.Models.AppContext;

public class AppointmentRepository : BaseRepository<Appointment, AppointmentID>, IAppointmentRepository
{
    private readonly AppContext context;

    public AppointmentRepository(AppContext context) : base(context.Appointments)
    {
        this.context = context;
    }
}