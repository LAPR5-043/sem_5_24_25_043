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
using Newtonsoft.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;


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
        private readonly HttpClient _httpClient;
        private static string servers = "servers.json";
        private static string better = "planning_better";
        private static string update = "planning_update";
        
        public static string nothing_to_schedule = "Nothing To Schedule";

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
            this._httpClient = new HttpClient();
        }
        public async Task<PlanningResponseDto> GenerateApointmentsByRoomAndDateAsync(string RoomId, int date)
        {
            ScheduleDto schedule = PrepareDataForPlanningModule(RoomId, date);
            if (schedule == null)
            {
                return null;
            }

            string apiUrl = GetApiUrlFromJsonFile(servers, update);

            bool status = await SendingPlanningModuleUpdatedData(apiUrl, schedule);

            if (!status)
            {
                return null;
            }

            apiUrl = GetApiUrlFromJsonFile(servers, better);

           PlanningResponseDto response = await GetPlanningModuleSchedulingAsync(apiUrl, RoomId, date);

            if (response.FinalOperationTime == 1441)
            {
                return null;
            }

            status = await TransformResponseintoAppointementsAsync(response, date);

            if (!status)
            {
                return null;
            }

            return response;
          
        }

        private async Task<bool> TransformResponseintoAppointementsAsync(PlanningResponseDto response, int date)
        {
            try
            {
                foreach (var surgery in response.OperationRoomAgenda)
                {
                    bool opReq = appointmentRepository.CheckIfOperationIsScheduled(surgery.Surgery).Result;

                    if (!opReq)
                    {
                        Appointment appoint = new Appointment
                        {
                            dateAndTime = new DateAndTime
                            {
                                startT = surgery.Start.ToString(),
                                endT = surgery.End.ToString(),
                                date = date.ToString()
                            },
                            requestID = surgery.Surgery,
                            roomID = response.Room,
                            status = Status.Scheduled
                        };

                        await appointmentRepository.AddAsync(appoint);
                        await unitOfWork.CommitAsync();
                    }


                }

                
            }
            catch (Exception e)
            {
                // Log the exception
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        private string GetApiUrlFromJsonFile(string filePath, string key)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file {filePath} does not exist.");
            }

            var json = File.ReadAllText(filePath);
            var jsonObject = JObject.Parse(json);

            if (jsonObject[key] == null)
            {
                throw new KeyNotFoundException($"The key '{key}' was not found in the JSON file.");
            }

            return jsonObject[key].ToString();
        }
        private async Task<bool> SendingPlanningModuleUpdatedData(string apiUrl, ScheduleDto schedule)
        {
            Console.WriteLine("Sending data to planning module...");
            var json = JsonConvert.SerializeObject(schedule);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, data);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var planningResponse = JsonConvert.DeserializeObject<StatusDto>(responseContent);
            if (planningResponse.Status.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }
       
        private async Task<PlanningResponseDto> GetPlanningModuleSchedulingAsync(string apiUrl, String roomId, int date)
        {
            Console.WriteLine("Getting data from planning module...");
            var json = JsonConvert.SerializeObject( new { room = roomId, day = date.ToString() } );
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, data);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var planningResponse = JsonConvert.DeserializeObject<PlanningResponseDto>(responseContent);


            Console.WriteLine("Data received from planning module...");
            return planningResponse;
        }

        private ScheduleDto PrepareDataForPlanningModule(String RoomId, int date){
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
                
                if (notScheduled.Count == 0)
                {
                    throw new Exception(nothing_to_schedule);
                }

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
                    temp.Agenda = "[";
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

                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda += "(" + start + "," + end + ","+req.RequestId+"),";
                            }
                            else if (staffSpecialization == "medical_action")
                            {
                                int start = etime - cleanT;
                                int end = etime;

                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda += "(" + start + "," + end + ","+req.RequestId+"),";

                            }
                            else
                            {
                                int start = stime + anestT;
                                int end = start + operT;

                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda += "(" + start + "," + end + ","+req.RequestId+"),";

                            }


                        }
                    }
                }
                foreach(var sta in  schedule.AgendaStaff)
                {   
                    int agendaLength = sta.Agenda.Length;

                    if(sta.Agenda[agendaLength-1] == ','){
                        sta.Agenda = sta.Agenda.Substring(0, agendaLength - 1) + "]";
                    }else{
                        sta.Agenda = sta.Agenda + "]";
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
                throw e;
            }

            return schedule;
        }    

    }
}
