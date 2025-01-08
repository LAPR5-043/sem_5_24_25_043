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
using Microsoft.AspNetCore.Mvc;
using Domain.PatientAggregate;
using Domain.StaffAggregate;


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
        private readonly IPatientRepository patientRepository;
        private readonly IStaffRepository staffRepository;
        private readonly ISurgeryRoomService roomService;
        private readonly IStaffService staffService;
        private readonly IOperationTypeService operationTypeService;
        private readonly IOperationRequestService operationRequestService;
        private readonly IPatientService patientService;
        private readonly IAvailabilitySlotService availabilitySlotService;
        private readonly ISpecializationService specializationService;
        private readonly HttpClient _httpClient;
        private static string servers = "servers.json";
        private static string better = "planning_better";
        private static string genetic = "planning_genetic";
        private static string update = "planning_update";

        public static string nothing_to_schedule = "Nothing To Schedule";
        public static string room_full = "The Room is Full";
        private static int minAverageAvailability = 700;
        private static int maxNumberOfOperations = 8;
        private int isGenetic = 0;

        /// <summary>
        /// Constructor
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="appointmentRepository"></param>
        public AppointmentService(IUnitOfWork unitOfWork, IAppointmentRepository appointmentRepository
                                     , ISurgeryRoomService roomService, IStaffService staffService, IOperationTypeService operationTypeService,
                                      IOperationRequestService operationRequestService, IAvailabilitySlotService availabilitySlotService,
                                      ISpecializationService specializationService, IPatientRepository patientRepository, IStaffRepository staffRepository, IPatientService patientService)
        {
            this.roomService = roomService;
            this.staffService = staffService;
            this.operationTypeService = operationTypeService;
            this.operationRequestService = operationRequestService;
            this.availabilitySlotService = availabilitySlotService;
            this.specializationService = specializationService;
            this.unitOfWork = unitOfWork;
            this.appointmentRepository = appointmentRepository;
            this.staffRepository = staffRepository;
            this.patientRepository = patientRepository;
            this.patientService = patientService;
            this._httpClient = new HttpClient();
        }

        public async Task<List<AppointmentDto>> GetAllAppointmentsAsync()
        {
            try
            {
                var appointments = await appointmentRepository.GetAllAsync();
                var operationRequests = await operationRequestService.GetAllOperationRequestsAsync();
                var patients = await patientRepository.GetAllAsync();
                var doctors = await staffRepository.GetAllAsync();

                Console.WriteLine("Appointments: " + appointments.Count());
                Console.WriteLine("Operation Requests: " + operationRequests.Count());
                Console.WriteLine("Patients: " + patients.Count());
                Console.WriteLine("Doctors: " + doctors.Count());

                List<AppointmentDto> appointmentsDto = new List<AppointmentDto>();

                foreach (var appoint in appointments)
                {
                    AppointmentDto appointDto = new AppointmentDto(appoint);
                    var request = operationRequests.FirstOrDefault(x => x.operationRequestID.ID == appoint.requestID.ToString());
                    if (request == null)
                    {
                        throw new InvalidOperationException("Operation request not found.");
                    }

                    appointDto.Request = new OperationRequestDto(request){
                        PatientName = (await patientService.GetPatientByIdAsync(request.patientID)).FullName,
                        DoctorName = (await staffService.GetStaffAsync(request.doctorID)).FullName
                    };

                    //appointDto.Request.DoctorName = doctors.FirstOrDefault(x => x.staffID.Value == request.doctorID.ToString())?.fullName;
                    //appointDto.Request.PatientName = patients.FirstOrDefault(x => x.MedicalRecordNumber.medicalRecordNumber == request.patientID.ToString())?.FullName.Value;

                    appointDto.Request.specializationsStaffNames = request.specializations?.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Select(staffId => new StaffInOperation { staffID = staffId, staffName = staffService.GetStaffAsync(staffId).Result.FullName }).ToList()
                    ) ?? new Dictionary<string, List<StaffInOperation>>();

                    if (appointDto.Request == null)
                    {
                        throw new InvalidOperationException("Operation request not found.");
                    }

                    appointmentsDto.Add(appointDto);
                }

                return appointmentsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
        public List<AppointmentDto> GetDayAppointmentsAsync(int day)
        {
            IEnumerable<Appointment> appointments = appointmentRepository.GetDayAppointmentsAsync(day).Result;
            List<AppointmentDto> appointmentsDto = new List<AppointmentDto>();

            foreach (var appoint in appointments)
            {
                AppointmentDto appointDto = new AppointmentDto(appoint);
                appointDto.Request = operationRequestService.GetOperationRequestByIdAsync(appoint.requestID).Result;

                appointmentsDto.Add(appointDto);
            }
            return appointmentsDto;

        }
        public async Task<PlanningResponseDto> GenerateApointmentsByRoomAndDateAsync(string RoomId, int date)
        {
            isGenetic = 0;
            ScheduleDto schedule = PrepareDataForPlanningModule(date);
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
                throw new Exception(room_full);
            }

            status = await TransformResponseintoAppointementsAsync(response, date);

            if (!status)
            {
                return null;
            }

            return response;

        }

        public async Task<GeneticResponseDto> GenerateApointmentsByDateAsync(int date)
        {
            isGenetic = 1;
            ScheduleDto schedule = PrepareDataForPlanningModule(date);
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

            apiUrl = GetApiUrlFromJsonFile(servers, genetic);

            GeneticResponseDto response = await GetPlanningModuleAllRoomsSchedulingAsync(apiUrl, date);

            if (response.OperationRoomAgenda == null)
            {
                return null;
            }


            status = await TransformGeneticResponseintoAppointementsAsync(response, date);

            if (!status)
            {
                return null;
            }

            return response;

        }


        private async Task<bool> TransformGeneticResponseintoAppointementsAsync(GeneticResponseDto response, int date)
        {
            try
            {
                foreach (var room in response.OperationRoomAgenda)
                {
                    foreach (var surgery in room.Agenda)
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
                                roomID = room.Name,
                                status = Status.Scheduled
                            };

                            await appointmentRepository.AddAsync(appoint);
                            await unitOfWork.CommitAsync();
                        }


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
            try
            {
                var json = JsonConvert.SerializeObject(schedule);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                Console.WriteLine(json);
                var response = await _httpClient.PostAsync(apiUrl, data);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {responseContent}");

                var planningResponse = JsonConvert.DeserializeObject<StatusDto>(responseContent);
                if (planningResponse.Status.IsNullOrEmpty())
                {
                    return false;
                }

                return true;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Error: {jsonEx.Message}");
                throw;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request Error: {httpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }

        private async Task<PlanningResponseDto> GetPlanningModuleSchedulingAsync(string apiUrl, String roomId, int date)
        {
            Console.WriteLine("Getting data from planning module...");
            var json = JsonConvert.SerializeObject(new { room = roomId, day = date.ToString() });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, data);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var planningResponse = JsonConvert.DeserializeObject<PlanningResponseDto>(responseContent);


            Console.WriteLine("Data received from planning module...");
            return planningResponse;
        }

        private async Task<GeneticResponseDto> GetPlanningModuleAllRoomsSchedulingAsync(string apiUrl, int date)
        {
            try
            {
                Console.WriteLine("Getting data from planning module...");
                var json = JsonConvert.SerializeObject(new { day = date.ToString() });
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(apiUrl, data);
                Console.WriteLine(response);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
                var planningResponse = JsonConvert.DeserializeObject<GeneticResponseDto>(responseContent);


                Console.WriteLine("Data received from planning module...");
                return planningResponse;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"HTTP Request Error: {httpEx.Message}");
                throw;
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON Error: {jsonEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }
        private List<OperationRequestDto> sortOperationRequestsByPriority(List<OperationRequest> opRequests, IEnumerable<OperationTypeDto> operationTypes)
        {
            SortedSet<OperationRequest> opRequestsSorted = new SortedSet<OperationRequest>(new OperationRequestComparer());
            foreach (var op in opRequests)
            {
                opRequestsSorted.Add(op);
            }

            List<OperationRequestDto> operationRequests = new List<OperationRequestDto>();



            foreach (var op in opRequestsSorted)
            {
                OperationRequestDto opDto = new OperationRequestDto(op);
                operationRequests.Add(opDto);
                OperationTypeDto opType = operationTypes.Where(x => x.OperationTypeName == op.operationTypeID).FirstOrDefault();
            }

            return operationRequests;
        }

        private List<OperationRequestDto> onlyScheduleSome(List<OperationRequestDto> opRequests, IEnumerable<OperationTypeDto> operationTypes)
        {
            List<OperationRequestDto> toSchedule = new List<OperationRequestDto>();

            int totalTime = 0;
            int numOp = 0;

            if (isGenetic == 0)
            {
                while (totalTime <= minAverageAvailability && opRequests.Count > 0 && maxNumberOfOperations > numOp)
                {
                    OperationRequestDto op = opRequests.First();
                    opRequests.Remove(op);
                    toSchedule.Add(op);
                    OperationTypeDto opType = operationTypes.Where(x => x.OperationTypeName == op.OperationTypeID).FirstOrDefault();
                    totalTime += int.Parse(opType.EstimatedDurationAnesthesia) + int.Parse(opType.EstimatedDurationOperation) + int.Parse(opType.EstimatedDurationCleaning);
                    numOp++;
                }
            }
            if (isGenetic == 1)
            {
                while (opRequests.Count > 0)
                {
                    OperationRequestDto op = opRequests.First();
                    opRequests.Remove(op);
                    toSchedule.Add(op);
                }
            }

            return toSchedule;
        }

        private ScheduleDto PrepareDataForPlanningModule(int date)
        {
            ScheduleDto schedule = new ScheduleDto();

            try
            {
                List<SurgeryRoomDto> rooms = roomService.GetSurgeryRoomsAsync().Result;
                //SurgeryRoomDto room = roomService.GetSurgeryRoomAsync(RoomId).Result;
                IEnumerable<StaffDto> staff = staffService.getStaffsFilteredAsync(null, null, null, null, null).Result;
                IEnumerable<OperationTypeDto> operationTypes = operationTypeService.getAllOperationTypesAsync().Result?.Value.Cast<OperationTypeDto>() ?? Enumerable.Empty<OperationTypeDto>();
                List<OperationRequest> opRequests = operationRequestService.GetAllOperationRequestsAsync().Result;

                List<OperationRequestDto> operationRequests = sortOperationRequestsByPriority(opRequests, operationTypes);


                List<AvailabilitySlot> availabilitySlots = availabilitySlotService.GetAllAvailabilitySlotsAsync().Result;
                List<SpecializationDto> specializations = specializationService.GetSpecializationsAsync().Result;
                List<Appointment> appointments = appointmentRepository.GetAllAsync().Result;

                List<string> opTypes = new List<string>();

                foreach (var r in rooms)
                {
                    schedule.AgendaOperationRoom.Add(new OperationRoom
                    {
                        room_id = r.RoomID,
                        date = date.ToString(),
                        agenda = "["
                    });
                }

                foreach (var app in appointments)
                {
                    if (app.dateAndTime.date == date.ToString())
                    {
                        OperationRoomAgendaDto temp = new OperationRoomAgendaDto();
                        temp.Start = int.Parse(app.dateAndTime.startT);
                        temp.End = int.Parse(app.dateAndTime.endT);
                        temp.Surgery = app.requestID;
                        schedule.AgendaOperationRoom.Where(x => x.room_id == app.roomID).FirstOrDefault().agenda += "(" + temp.Start + "," + temp.End + "," + temp.Surgery + "),";

                    }

                }

                foreach (var r in schedule.AgendaOperationRoom)
                {

                    int length = r.agenda.Length - 1;

                    if (r.agenda[length] == ',')
                    {
                        r.agenda = r.agenda.Substring(0, length) + "]";
                    }
                    else
                    {
                        r.agenda = r.agenda + "]";
                    }

                }

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
                    else
                    {

                        TimetableDto temp = new TimetableDto();
                        temp.Id = slot.Id.ToString();
                        temp.Date = date.ToString();
                        temp.Time = "(" + 0 + "," + 1400 + ")";
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

                List<OperationRequestDto> notScheduled = onlyScheduleSome(operationRequests.AsQueryable().Where(x => !requestsWichAlreadyAreScheduled.Contains(x.RequestId)).ToList(), operationTypes);


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


                            if (staffSpecialization == null)
                            {
                                Console.WriteLine(assignedStaff);
                            }

                            if (staffSpecialization == "anaesthetist")
                            {
                                int start = stime;
                                int end = start + anestT + operT;

                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda += "(" + start + "," + end + "," + req.RequestId + "),";
                            }
                            else if (staffSpecialization == "medical_action")
                            {
                                int start = etime - cleanT;
                                int end = etime;

                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda += "(" + start + "," + end + "," + req.RequestId + "),";

                            }
                            else
                            {
                                int start = stime + anestT;
                                int end = start + operT;

                                schedule.AgendaStaff.AsQueryable().Where(x => x.Id.ToLower() == assignedStaff.ToLower()).FirstOrDefault().Agenda += "(" + start + "," + end + "," + req.RequestId + "),";

                            }


                        }
                    }
                }
                foreach (var sta in schedule.AgendaStaff)
                {
                    int agendaLength = sta.Agenda.Length;

                    if (sta.Agenda[agendaLength - 1] == ',')
                    {
                        sta.Agenda = sta.Agenda.Substring(0, agendaLength - 1) + "]";
                    }
                    else
                    {
                        sta.Agenda = sta.Agenda + "]";
                    }

                }

                foreach (var op in operationTypes)
                {
                    SurgeryDto temp = new SurgeryDto();
                    temp.Id = op.OperationTypeName.ToString();
                    temp.Duration = op.EstimatedDurationAnesthesia;
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

        public Task<AppointmentDto> GetAppointmentByRequestIDAsync(string requestID)
        {
            var appoint = appointmentRepository.GetAppointmentByRequestID(requestID).Result;
            if (appoint == null)
            {
                throw new Exception("Appointment not found");
            }
            AppointmentDto appointDto = new AppointmentDto(appoint);

            appointDto.Request = operationRequestService.GetOperationRequestByIdAsync(appoint.requestID).Result;
            return Task.FromResult<AppointmentDto>(appointDto);
        }

        public async Task<AppointmentDto> createAppointmentAsync(AppointmentDto appointmentDto)
        {
            try
            {
                bool opReq = appointmentRepository.CheckIfOperationIsScheduled(int.Parse(appointmentDto.Request.RequestId)).Result;
                if (opReq)
                {
                    throw new Exception("Operation already scheduled.");
                }
                if (!checkIfItTheScheduleIsAvailable(appointmentDto.DateAndTime, appointmentDto.RoomID))
                {
                    throw new Exception("The schedule is not available.");
                }
                Appointment appoint = new Appointment
                {
                    dateAndTime = new DateAndTime
                    {
                        startT = appointmentDto.DateAndTime.StartT,
                        endT = appointmentDto.DateAndTime.EndT,
                        date = appointmentDto.DateAndTime.Date
                    },
                    requestID = int.Parse(appointmentDto.Request.RequestId),
                    roomID = appointmentDto.RoomID,
                    status = Status.Scheduled
                };
                await appointmentRepository.AddAsync(appoint);
                await unitOfWork.CommitAsync();
                AppointmentDto result = new AppointmentDto(appoint);
                result.Request = operationRequestService.GetOperationRequestByIdAsync(appoint.requestID).Result;
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<AppointmentDto> updateAppointmentAsync(AppointmentDto appointmentDto)
        {
            if (appointmentDto == null)
            {
                throw new ArgumentNullException(nameof(appointmentDto), "Appointment data is null.");
            }

            var appointment = await appointmentRepository.GetAppointmentByRequestID(appointmentDto.Request.RequestId);

            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }
            Console.WriteLine("1");
            // Atualizar as propriedades da Appointment com os valores do AppointmentDto
            if (appointmentDto.DateAndTime != null)
            {
                if (!checkIfItTheScheduleIsAvailable(appointmentDto.DateAndTime, appointmentDto.RoomID))
                {
                    throw new Exception("The schedule is not available.");
                }
                appointment.dateAndTime.startT = appointmentDto.DateAndTime.StartT;
                appointment.dateAndTime.endT = appointmentDto.DateAndTime.EndT;
                appointment.dateAndTime.date = appointmentDto.DateAndTime.Date;
            }
            Console.WriteLine("2");
            if (appointmentDto.Request != null)
            {
                appointment.requestID = int.Parse(appointmentDto.Request.RequestId);
            }
            Console.WriteLine("3");
            if (appointmentDto.RoomID != null)
            {
                appointment.roomID = appointmentDto.RoomID;
            }

            if (appointmentDto.Status != null)
            {
                appointment.status = Enum.Parse<Status>(appointmentDto.Status);
            }

            Console.WriteLine("4");

            // Salvar as alterações no repositório
            await appointmentRepository.updateAsync(appointment);
            await unitOfWork.CommitAsync();
            Console.WriteLine("5");
            var result = new AppointmentDto(appointment);
            result.Request = operationRequestService.GetOperationRequestByIdAsync(appointment.requestID).Result;
            return result;
        }

        public bool checkIfItTheScheduleIsAvailable(DateAndTimeDto time, string roomID)
        {
            IEnumerable<Appointment> appointments = appointmentRepository.GetAllAsync().Result;
            foreach (var appoint in appointments)
            {
                if (appoint.roomID == roomID && appoint.dateAndTime.date == time.Date)
                {
                    if (int.Parse(appoint.dateAndTime.startT) <= int.Parse(time.StartT) && int.Parse(appoint.dateAndTime.endT) >= int.Parse(time.StartT))
                    {
                        return false;
                    }
                    if (int.Parse(appoint.dateAndTime.startT) <= int.Parse(time.EndT) && int.Parse(appoint.dateAndTime.endT) >= int.Parse(time.EndT))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
