using System;
using System.Reflection;
using Domain.PatientAggregate;
using Microsoft.AspNetCore.Mvc;
using sem_5_24_25_043;
using src.Domain.PatientAggregate;
using src.Domain.Shared;
using src.Services.IServices;


namespace src.Services.Services
{
    public class PatientService : IPatientService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IPatientRepository patientRepository;
        private readonly ILogService logService;
        private readonly AuthService authService;
        private readonly ISensitiveDataService sensitiveDataService;
        private readonly IPendingRequestService pendingRequestService;
        private readonly IEmailService emailService;
        private static string patientDeleteLog1 = "Patient deleted with success;PatientId:";

        public PatientService(IUnitOfWork unitOfWork, IPatientRepository patientRepository, ILogService logService, IPendingRequestService pendingRequestService, IEmailService emailService, AuthService authService)
        {
            this.unitOfWork = unitOfWork;
            this.patientRepository = patientRepository;
            this.logService = logService;
            this.sensitiveDataService = new SensitiveDataService();
            this.pendingRequestService = pendingRequestService;
            this.emailService = emailService;
            this.authService = authService;
        }

        public async Task<OkObjectResult> GetAllPatientsAsync()
        {
            var result = await patientRepository.GetAllAsync();
            IEnumerable<PatientDto> resultDtos = new List<PatientDto>();
            foreach (var patient in result)
            {
                resultDtos.Append(new PatientDto(patient));
            }
            return new OkObjectResult(resultDtos);

        }


        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatientsFilteredAsync(string? firstName, string? lastName, string? email, string? phoneNumber, string? medicalRecordNumber, string? dateOfBirth, string? gender, string? sortBy)
        {
            bool ascending = true;
            var patientList = await patientRepository.GetAllAsync();
            var query = patientList.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(p => p.firstName.firstName.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(p => p.lastName.lastName.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.email.email.Contains(email));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(p => p.phoneNumber.phoneNumber.Contains(phoneNumber));
            }

            if (!string.IsNullOrEmpty(medicalRecordNumber))
            {
                query = query.Where(p => p.medicalRecordNumber.medicalRecordNumber.Contains(medicalRecordNumber));
            }

            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                query = query.Where(p => p.dateOfBirth.ToString().Contains(dateOfBirth));
            }

            if (!string.IsNullOrEmpty(gender) && Enum.TryParse(gender, true, out Gender genderEnumValue))
            {
                query = query.Where(p => p.gender == genderEnumValue);
            }



            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "firstname":
                        query = query.OrderBy(p => p.firstName.firstName);
                        break;
                    case "lastname":
                        query = query.OrderBy(p => p.lastName.lastName);
                        break;
                    case "email":
                        query = query.OrderBy(p => p.email.email);
                        break;
                    case "phonenumber":
                        query = query.OrderBy(p => p.phoneNumber.phoneNumber);
                        break;
                    case "medicalrecordnumber":
                        query = query.OrderBy(p => p.medicalRecordNumber.medicalRecordNumber);
                        break;
                    case "dateofbirth":
                        query = query.OrderBy(p => p.dateOfBirth.dateOfBirth);
                        break;
                    case "gender":
                        query = query.OrderBy(p => p.gender);
                        break;
                    default:
                        query = query.OrderBy(p => p.firstName.firstName);
                        break;
                }
            }

            var result = query.ToList();
            var resultDtos = result.Select(s => new PatientDto(s)).ToList();

            return resultDtos;
        }

        public Task<PatientDto> GetPatientByIdAsync(string id)
        {
            var patient = patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            return Task.FromResult(new PatientDto(patient.Result));
        }

        public async Task<bool> DeletePatientAsync(string id)
        {
            var patient = await patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
            if (patient == null)
            {
                return false;
            }
            patientRepository.Remove(patient);
            await unitOfWork.CommitAsync();

            await logService.CreateLogAsync("Patient deleted with success;PatientId:" + id, "colocar@emailtoken.aqui");
            return true;
        }

        /// <summary>
        /// Create a new patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<PatientDto> CreatePatientAsync(PatientDto patient)
        {

            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient data is null.");
            }

            Boolean validation = ValidatePatientInformation(patient.Email, patient.PhoneNumber);

            if (validation)
            {
                throw new InvalidOperationException("Patient already exists.");
            }

            var newPatient = new Patient();
            newPatient.firstName = new PatientFirstName(patient.FirstName);
            newPatient.lastName = new PatientLastName(patient.LastName);
            newPatient.fullName = new PatientFullName(patient.FirstName, patient.LastName);
            newPatient.email = new PatientEmail(patient.Email);
            newPatient.phoneNumber = new PatientPhoneNumber(patient.PhoneNumber);
            newPatient.emergencyContact = new EmergencyContact(patient.EmergencyContactName, patient.EmergencyContactPhoneNumber);
            newPatient.dateOfBirth = new DateOfBirth(patient.DayOfBirth, patient.MonthOfBirth, patient.YearOfBirth);
            newPatient.gender = GenderExtensions.FromString(patient.Gender);


            await patientRepository.AddAsync(newPatient);
            await unitOfWork.CommitAsync();

            return new PatientDto(newPatient);
        }

        private Boolean ValidatePatientInformation(string email, string phoneNumber)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("Invalid patient data.");
            }

            if (!email.Contains("@"))
            {
                throw new Exception("Invalid email.");
            }

            if (phoneNumber.Length != 9)
            {
                throw new Exception("Invalid phone number.");
            }

            return patientRepository.PatientExists(email, phoneNumber);
        }

        /*public Task<Patient> GetPatientByIdAsync(string id)
        {
            var patient = patientRepository.GetByIdAsync(new MedicalRecordNumber(id.ToString()));
            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            return Task.FromResult(patient.Result);
        }*/

        public async Task<bool> UpdatePatientAsync(string id, PatientDto patientDto)
        {
            bool result;
            List<long> pendingRequestIds = new List<long>();
            Patient patientTemp = dtoToPatient(patientDto);

            if (patientDto == null)
            {
                throw new ArgumentNullException(nameof(patientDto), "Patient data is null.");
            }

            var patient = await patientRepository.GetByIdAsync(new MedicalRecordNumber(id));


            if (patient == null)
            {

                throw new Exception("Patient not found.");
            }

            Patient patient1 = patient;

            try
            {
                if (patient1 != null)
                {
                    foreach (PropertyInfo property in patient1.GetType().GetProperties())
                    {
                        var propertyName = property.Name;
                        var propertyValue = patient.GetType().Name + "." + propertyName;

                        if (sensitiveDataService.isSensitive(propertyValue))
                        {
                            var tempValue = property.GetValue(patientTemp);
                            var originalValue = property.GetValue(patient1);

                            if (tempValue != null && !tempValue.Equals(originalValue))
                            {
                                PendingRequest p = await pendingRequestService.AddPendingRequestAsync(
                                    patient1.medicalRecordNumber.AsString(),
                                    originalValue.ToString(),
                                    tempValue.ToString(),
                                    propertyValue
                                );

                                pendingRequestIds.Add(p.requestID.Value);
                            }
                        }
                        else
                        {

                            var tempValue = property.GetValue(patientTemp);
                            var originalValue = property.GetValue(patient1);

                            if (tempValue != null && !tempValue.Equals(originalValue))
                            {
                                if (property.PropertyType.IsAssignableFrom(tempValue.GetType()))
                                {

                                    property.SetValue(patient1, tempValue);

                                    await logService.CreateLogAsync(
                                         "Patient updated with success;PatientId:" + id + ";Value Changed:" + property.Name + ";NewValue:" + tempValue.ToString(),
                                         patient1.email.ToString()
                                     );
                                     await unitOfWork.CommitAsync();
                                }
                                else
                                {
                                    Console.WriteLine($"Type mismatch for property {propertyName}. Expected {property.PropertyType}, but got {tempValue.GetType()}.");
                                }
                            }
                        }
                    }

                    string url = buildUrl(pendingRequestIds);
                    await emailService.SendEmailAsync(patient1.email.ToString(), "Patient data update", url);
                }
                await unitOfWork.CommitAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;

            }


            return true;


        }

        private string buildUrl(List<long> pendingRequestIds)
        {
            string url = "https://localhost:7258/api/patient/acceptPendingRequests?requestIds=";
            for (int i = 0; i < pendingRequestIds.Count; i++)
            {
                if (i == pendingRequestIds.Count - 1)
                {
                    url += pendingRequestIds[i];
                    break;
                }
                else
                {
                    url += pendingRequestIds[i] + ",";
                }
            }
            return url;
        }

        private Patient dtoToPatient(PatientDto patientDto)
        {
            return new Patient
            {
                medicalRecordNumber = string.IsNullOrEmpty(patientDto.MedicalRecordNumber) ? null : new MedicalRecordNumber(patientDto.MedicalRecordNumber),
                firstName = string.IsNullOrEmpty(patientDto.FirstName) ? null : new PatientFirstName(patientDto.FirstName),
                lastName = string.IsNullOrEmpty(patientDto.LastName) ? null : new PatientLastName(patientDto.LastName),
                fullName = string.IsNullOrEmpty(patientDto.FirstName) || string.IsNullOrEmpty(patientDto.LastName) ? null : new PatientFullName(patientDto.FirstName, patientDto.LastName),
                email = string.IsNullOrEmpty(patientDto.Email) ? null : new PatientEmail(patientDto.Email),
                phoneNumber = string.IsNullOrEmpty(patientDto.PhoneNumber) ? null : new PatientPhoneNumber(patientDto.PhoneNumber),
                emergencyContact = string.IsNullOrEmpty(patientDto.EmergencyContactName) || string.IsNullOrEmpty(patientDto.EmergencyContactPhoneNumber) ? null : new EmergencyContact(patientDto.EmergencyContactName, patientDto.EmergencyContactPhoneNumber),
                dateOfBirth = string.IsNullOrEmpty(patientDto.DayOfBirth) || string.IsNullOrEmpty(patientDto.MonthOfBirth) || string.IsNullOrEmpty(patientDto.YearOfBirth) ? null : new DateOfBirth(patientDto.DayOfBirth, patientDto.MonthOfBirth, patientDto.YearOfBirth),
                gender = GenderExtensions.FromString(patientDto.Gender),
                allergiesAndConditions = patientDto.AllergiesAndConditions == null ? null : dtoToAllergiesAndConditions(patientDto.AllergiesAndConditions),
                appointmentHistory = patientDto.AppointmentHistory == null ? null : new AppointmentHistory()
            };
        }

        private List<AllergiesAndConditions> dtoToAllergiesAndConditions(List<string> allergiesAndConditions)
        {
            List<AllergiesAndConditions> allergiesAndConditionsList = new List<AllergiesAndConditions>();
            if (allergiesAndConditions == null || allergiesAndConditions.Count == 0)
            {
                return allergiesAndConditionsList;
            }
            foreach (string allergyOrCondition in allergiesAndConditions)
            {
                if (!string.IsNullOrEmpty(allergyOrCondition))
                {
                    allergiesAndConditionsList.Add(new AllergiesAndConditions(allergyOrCondition));
                }

            }

            return allergiesAndConditionsList;
        }

        public Task<Patient> GetPatientEntityByIdAsync(string id)
        {
            var patient = patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
            if (patient == null)
            {
                throw new Exception("Patient not found.");
            }

            return Task.FromResult(patient.Result);
        }

        public bool AcceptRequests(List<long> requestIds)
        {


            foreach (long pendingRequest in requestIds)
            {
                PendingRequest request = pendingRequestService.GetByIdAsync(new LongId(pendingRequest));
                if (request == null)
                {
                    return false;
                }

                Patient p = GetPatientEntityByIdAsync(request.userId).Result;

                PropertyInfo property = p.GetType().GetProperty(request.attributeName.Split(".")[1]);
                if (property != null && property.CanWrite)
                {
                    object value = ConvertToCustomType(request.pendingValue, property.PropertyType);
                    property.SetValue(p, value, null);
                    logService.CreateLogAsync("PatientId:" + request.userId + ";Attribute:" + request.attributeName + ";updated with success;", p.email.email);
                }
                else
                {
                    return false;
                }
            }

            unitOfWork.CommitAsync().Wait();
            return true;
        }

        private object ConvertToCustomType(string value, Type targetType)
        {
            // Check if the target type has a constructor that accepts a string
            ConstructorInfo constructor = targetType.GetConstructor(new[] { typeof(string) });
            if (constructor != null)
            {
                return constructor.Invoke(new object[] { value });
            }

            // Check if the target type has a static method 'FromString' that accepts a string
            MethodInfo fromStringMethod = targetType.GetMethod("FromString", BindingFlags.Static | BindingFlags.Public);
            if (fromStringMethod != null)
            {
                return fromStringMethod.Invoke(null, new object[] { value });
            }

            // Fallback to default conversion
            return Convert.ChangeType(value, targetType);
        }

        public async Task RegisterNewPatientIAMAsync(string email, string patientEmail, string password)
        {
            var result = patientRepository.PatientExists(patientEmail);

            Console.WriteLine("Patient Exists: " + result);

            if (!result)
            {
                throw new InvalidOperationException("Patient Does Not Exist");
            }

            await authService.RegisterNewPatientAsync(email, patientEmail, password);

            Console.WriteLine("Patient Registered in IAM System");

            await unitOfWork.CommitAsync();

            await logService.CreateLogAsync("New patient registered in the IAM system; PatientEmail:" + email, email);
        }
    }
}