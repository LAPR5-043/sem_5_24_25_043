using System;
using Domain.AppointmentAggregate;
using src.Domain.Shared;
using src.Services.IServices;
using sem_5_24_25_043.Domain.AppointmentAggregate;
using src.Domain.AppointmentAggregate;
using Domain.SurgeryRoomAggregate;
using src.Domain.SurgeryRoomAggregate;
using Domain.OperationRequestAggregate;
using src.Domain.AvailabilitySlotAggregate;
using Schedule;
using System.Linq.Expressions;
using Domain.OperationTypeAggregate;
using NuGet.Protocol;


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
        private readonly ISurgeryRoomService roomService;
        private readonly IStaffService staffService;
        private readonly IOperationTypeService operationTypeService;
        private readonly IOperationRequestService operationRequestService;
        private readonly IAvailabilitySlotService availabilitySlotService;
        private readonly ISpecializationService specializationService;

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="appointmentRepository"></param>
        public AppointmentService(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository
                                     , ISurgeryRoomService roomService, IStaffService staffService, IOperationTypeService operationTypeService,
                                      IOperationRequestService operationRequestService, IAvailabilitySlotService availabilitySlotService, 
                                      ISpecializationService specializationService)
        {
            this.roomService = roomService;
            this.staffService = staffService;
            this.operationTypeService = operationTypeService;
            this.operationRequestService = operationRequestService;
            this.availabilitySlotService = availabilitySlotService;
            this.specializationService = specializationService;
            this.unitOfWork = unitOfWork;
            this.appointmentRepository = appointmentRepository;
        }
        public ScheduleDto GenerateApointmentsByRoomAndDate(String RoomId, int date)
        {
                ScheduleDto schedule = new ScheduleDto();
            try
            {
                SurgeryRoomDto room = roomService.GetSurgeryRoomAsync(RoomId).Result;
                IEnumerable<StaffDto> staff = staffService.getStaffsFilteredAsync(null, null, null, null, null).Result;
                IEnumerable<OperationTypeDto> operationTypes = operationTypeService.getAllOperationTypesAsync().Result?.Value.Cast<OperationTypeDto>() ?? Enumerable.Empty<OperationTypeDto>();
                List<OperationRequestDto> operationRequests = operationRequestService.GetOperationRequestFilteredAsync(null, null, null, null, null, null, "priority").Result;
                List<AvailabilitySlot> availabilitySlots = availabilitySlotService.GetAllAvailabilitySlotsAsync().Result;
                List<SpecializationDto> specializations = specializationService.GetSpecializationsAsync().Result;
                List<Appointment> appointments = appointmentRepository.GetAllAsync().Result;

                List<string> opTypes = new List<string>();
                foreach (var op in operationTypes)
                {
                    opTypes.Add(op.OperationTypeName.ToString().ToLower());
                }


                foreach (var slot in availabilitySlots)
                {
                    if (slot.Slots.ContainsKey(date))
                    {
                        Slot availability = slot.Slots[date];
                        TimetableDto temp = new TimetableDto();
                        temp.Id = slot.Id.ToString();
                        temp.Date = date.ToString();
                        temp.Time = "(" + availability.StartTime + "," + availability.EndTime + ")";
                        schedule.Timetables.Add(temp);
                    }
                }

                foreach (var st in staff)
                {
                    StaffDesc temp = new StaffDesc();
                    temp.Id = st.StaffID.ToString().ToLower();

                    if (temp.Id.Contains("d"))
                    {
                        temp.Role = "doctor";
                    }
                    else if (temp.Id.Contains("n"))
                    {
                        temp.Role = "nurse";
                    }
                    else
                    {
                        temp.Role = "assistant";
                    }

                    temp.Specialty = st.SpecializationID;
                    temp.Operations = opTypes;
                    schedule.Staff.Add(temp);
                }

                List<string> requestsWichAlreadyAreScheduled = new List<string>();
                foreach (var appoint in appointments)
                {
                    requestsWichAlreadyAreScheduled.Add(appoint.requestID.ToString());
                }

                List<OperationRequestDto> notScheduled = operationRequests.AsQueryable().Where(x => !requestsWichAlreadyAreScheduled.Contains(x.RequestId)).ToList();

                foreach (var request in notScheduled)
                {
                    string tempRequestId = request.RequestId;

                    foreach (var spec in request.specializationsStaff)
                    {
                        foreach (string assignedStaff in spec.Value)
                        {
                            AssignmentSurgeryDto temp = new AssignmentSurgeryDto();
                            temp.SurgeryId = tempRequestId;
                            temp.DoctorId = assignedStaff.ToLower();
                            schedule.AssignmentSurgery.Add(temp);
                        }
                    }
                }

                foreach (var request in notScheduled)
                {
                    SurgeryIdDto temp = new SurgeryIdDto();
                    temp.SurgeryId = request.RequestId;
                    temp.SurgeryType = request.OperationTypeID;
                    schedule.SurgeryId.Add(temp);
                }

                List<OperationRequestDto> AlreadyScheduled = operationRequests.AsQueryable().Where(x => requestsWichAlreadyAreScheduled.Contains(x.RequestId)).ToList();

                foreach (var st in staff)
                {
                    AgendaStaffDto temp = new AgendaStaffDto();
                    temp.Id = st.StaffID.ToString().ToLower();
                    temp.Date = date.ToString();
                    temp.Agenda = new List<string>();
                    schedule.AgendaStaff.Add(temp);
                }

                foreach (var req in AlreadyScheduled)
                {
                    OperationTypeDto opType = operationTypes.AsQueryable().Where(x => x.OperationTypeName.ToString() == req.OperationTypeID).FirstOrDefault();

                    int anestT = int.Parse(opType.EstimatedDurationAnesthesia);
                    int operT = int.Parse(opType.EstimatedDurationOperation);
                    int cleanT = int.Parse(opType.EstimatedDurationCleaning);

                    Appointment appoint = appointments.AsQueryable().Where(x => x.requestID.ToString() == req.RequestId).FirstOrDefault();

                    int stime = int.Parse(appoint.dateAndTime.startT);
                    int etime = int.Parse(appoint.dateAndTime.endT);

                    foreach (var spec in req.specializationsStaff)
                    {

                        foreach (string assignedStaff in spec.Value)
                        {

                            string staffSpecialization = staff.AsQueryable().Where(x => x.StaffID.ToString().ToLower() == assignedStaff.ToLower()).FirstOrDefault().SpecializationID;

                            if (staffSpecialization == "anaesthetist")
                            {
                                int start = stime;
                                int end = start + anestT + operT;
                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda.Add("(" + start + "," + end + ")");
                            }
                            else if (staffSpecialization == "medical_action")
                            {
                                int start = etime - cleanT;
                                int end = etime;
                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda.Add("(" + start + "," + end + ")");
                            }
                            else
                            {
                                int start = stime + anestT;
                                int end = start + operT;
                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda.Add("(" + start + "," + end + ")");
                            }


                        }
                    }
                }

                foreach (var op in operationTypes)
                {
                    SurgeryDto temp = new SurgeryDto();
                    temp.Id = op.OperationTypeName.ToString();
                    temp.Duration = op.EstimatedDurationAnesthesia ;
                    temp.Time = op.EstimatedDurationOperation;
                    temp.Cleaning = op.EstimatedDurationCleaning;
                    schedule.Surgery.Add(temp);
                }

            }
            catch (Exception e)
            {
                return null;
            }

            return schedule;
        }

    }
}
