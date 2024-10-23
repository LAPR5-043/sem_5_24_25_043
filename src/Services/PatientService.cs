using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Domain.PatientAggregate;
using Microsoft.AspNetCore.Mvc;
using sem_5_24_25_043;
using src.Domain.PatientAggregate;
using src.Domain.Shared;
using src.Services.IServices;


namespace src.Services.Services
{
    /// <summary>
    /// Patient service
    /// </summary>
    public class PatientService : IPatientService
    {
        /// <summary>
        /// Unit of work
        /// </summary>
        private readonly IUnitOfWork unitOfWork;
        /// <summary>
        /// Patient repository
        /// </summary>
        private readonly IPatientRepository patientRepository;
        /// <summary>
        /// Log service
        /// </summary>
        private readonly ILogService logService;
        /// <summary>
        /// Auth service
        /// </summary>
        private readonly AuthService authService;
        /// <summary>
        /// Sensitive data service
        /// </summary>
        private readonly ISensitiveDataService sensitiveDataService;
        /// <summary>
        /// Pending request service
        /// </summary>
        private readonly IPendingRequestService pendingRequestService;
        /// <summary>
        /// Email service
        /// </summary>
        private readonly IEmailService emailService;
        /// <summary>
        /// Patient delete log
        /// </summary>
        private static string patientDeleteLog1 = "Patient deleted with success;PatientId:";

        private static string url = "localhost:7258";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="patientRepository"></param>
        /// <param name="logService"></param>
        /// <param name="pendingRequestService"></param>
        /// <param name="emailService"></param>
        /// <param name="authService"></param>
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

        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatientsFilteredAsync(string? firstName, string? lastName, string? email, string? phoneNumber, string? medicalRecordNumber, string? dateOfBirth, string? gender, string? sortBy)
        {
            bool ascending = true;
            var patientList = await patientRepository.GetAllAsync();
            var query = patientList.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(p => p.FirstName.Value.Contains(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(p => p.LastName.Value.Contains(lastName));
            }

            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(p => p.Email.Value.Contains(email));
            }

            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query = query.Where(p => p.PhoneNumber.Value.Contains(phoneNumber));
            }

            if (!string.IsNullOrEmpty(medicalRecordNumber))
            {
                query = query.Where(p => p.MedicalRecordNumber.medicalRecordNumber.Contains(medicalRecordNumber));
            }

            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                query = query.Where(p => p.DateOfBirth.ToString().Contains(dateOfBirth));
            }

            if (!string.IsNullOrEmpty(gender) && Enum.TryParse(gender, true, out Gender genderEnumValue))
            {
                query = query.Where(p => p.Gender == genderEnumValue);
            }



            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "firstname":
                        query = query.OrderBy(p => p.FirstName.Value);
                        break;
                    case "lastname":
                        query = query.OrderBy(p => p.LastName.Value);
                        break;
                    case "email":
                        query = query.OrderBy(p => p.Email.Value);
                        break;
                    case "phonenumber":
                        query = query.OrderBy(p => p.PhoneNumber.Value);
                        break;
                    case "medicalrecordnumber":
                        query = query.OrderBy(p => p.MedicalRecordNumber.medicalRecordNumber);
                        break;
                    case "dateofbirth":
                        query = query.OrderBy(p => p.DateOfBirth.Value);
                        break;
                    case "gender":
                        query = query.OrderBy(p => p.Gender);
                        break;
                    default:
                        query = query.OrderBy(p => p.FirstName.Value);
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

        public async Task<bool> DeletePatientAsync(string id, string adminEmail)
        {
            var patient = await patientRepository.GetByIdAsync(new MedicalRecordNumber(id));
            if (patient == null)
            {
                return false;
            }
            patientRepository.Remove(patient);
            await unitOfWork.CommitAsync();

            await logService.CreateLogAsync("Patient deleted with success;PatientId:" + id, adminEmail);
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

            // Check if the patient already exists
            Boolean validation = ValidatePatientInformation(patient.Email, patient.PhoneNumber);

            if (validation)
            {
                throw new InvalidOperationException("Patient already exists.");
            }

            var newPatient = new Patient();
            newPatient.FirstName = new PatientFirstName(patient.FirstName);
            newPatient.LastName = new PatientLastName(patient.LastName);
            newPatient.FullName = new PatientFullName(patient.FirstName, patient.LastName);
            newPatient.Email = new PatientEmail(patient.Email);
            newPatient.PhoneNumber = new PatientPhoneNumber(patient.PhoneNumber);
            newPatient.EmergencyContact = new EmergencyContact(patient.EmergencyContactName, patient.EmergencyContactPhoneNumber);
            newPatient.DateOfBirth = new DateOfBirth(patient.DayOfBirth, patient.MonthOfBirth, patient.YearOfBirth);
            newPatient.Gender = GenderExtensions.FromString(patient.Gender);

            // Add the patient to the repository
            await patientRepository.AddAsync(newPatient);
            await unitOfWork.CommitAsync();

            return new PatientDto(newPatient);
        }
        /// <summary>
        /// Validate if the patient already exists
        /// </summary>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        private Boolean ValidatePatientInformation(string email, string phoneNumber)
        {
            return patientRepository.PatientExists(email, phoneNumber);
        }

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
                        var propertyValue =  propertyName;

                        if (sensitiveDataService.isSensitive(propertyValue))
                        {
                            var tempValue = property.GetValue(patientTemp);
                            var originalValue = property.GetValue(patient1);

                            if (tempValue != null && !tempValue.Equals(originalValue))
                            {
                                PendingRequest p = await pendingRequestService.AddPendingRequestAsync(
                                    patient1.MedicalRecordNumber.AsString(),
                                    originalValue.ToString(),
                                    tempValue.ToString(),
                                    propertyValue
                                );

                              //  await unitOfWork.CommitAsync();

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
                                         patient1.Email.ToString()
                                     );
                                    
                                }
                                else
                                {
                                    Console.WriteLine($"Type mismatch for property {propertyName}. Expected {property.PropertyType}, but got {tempValue.GetType()}.");
                                }
                            }
                        }
                    }

                    if (pendingRequestIds.Count > 0){}
                        string url = buildUrl(pendingRequestIds);
                        await emailService.SendEmailAsync(patient1.Email.ToString(), "Patient data update", url);
                        
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
            string urls = "https://"+url+"/api/patient/acceptPendingRequests?requestIds=";
            for (int i = 0; i < pendingRequestIds.Count; i++)
            {
                if (i == pendingRequestIds.Count - 1)
                {
                    urls += pendingRequestIds[i];
                    break;
                }
                else
                {
                    urls += pendingRequestIds[i] + ",";
                }
            }
            return urls;
        }

        private Patient dtoToPatient(PatientDto patientDto)
        {
            return new Patient
            {
                MedicalRecordNumber = string.IsNullOrEmpty(patientDto.MedicalRecordNumber) ? null : new MedicalRecordNumber(patientDto.MedicalRecordNumber),
                FirstName = string.IsNullOrEmpty(patientDto.FirstName) ? null : new PatientFirstName(patientDto.FirstName),
                LastName = string.IsNullOrEmpty(patientDto.LastName) ? null : new PatientLastName(patientDto.LastName),
                FullName = string.IsNullOrEmpty(patientDto.FirstName) || string.IsNullOrEmpty(patientDto.LastName) ? null : new PatientFullName(patientDto.FirstName, patientDto.LastName),
                Email = string.IsNullOrEmpty(patientDto.Email) ? null : new PatientEmail(patientDto.Email),
                PhoneNumber = string.IsNullOrEmpty(patientDto.PhoneNumber) ? null : new PatientPhoneNumber(patientDto.PhoneNumber),
                EmergencyContact = string.IsNullOrEmpty(patientDto.EmergencyContactName) || string.IsNullOrEmpty(patientDto.EmergencyContactPhoneNumber) ? null : new EmergencyContact(patientDto.EmergencyContactName, patientDto.EmergencyContactPhoneNumber),
                DateOfBirth = string.IsNullOrEmpty(patientDto.DayOfBirth) || string.IsNullOrEmpty(patientDto.MonthOfBirth) || string.IsNullOrEmpty(patientDto.YearOfBirth) ? null : new DateOfBirth(patientDto.DayOfBirth, patientDto.MonthOfBirth, patientDto.YearOfBirth),
                Gender = GenderExtensions.FromString(patientDto.Gender),
                AllergiesAndConditions = patientDto.AllergiesAndConditions == null ? null : dtoToAllergiesAndConditions(patientDto.AllergiesAndConditions),
                AppointmentHistory = patientDto.AppointmentHistory == null ? null : new AppointmentHistory()
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

        public async Task<bool> AcceptRequests(List<long> requestIds)
        {


            foreach (long pendingRequest in requestIds)
            {
                PendingRequest request = pendingRequestService.GetByIdAsync(new LongId(pendingRequest));
                if (request == null)
                {
                    return false;
                }

                Patient p = GetPatientEntityByIdAsync(request.userId).Result;

                PropertyInfo property = p.GetType().GetProperty(request.attributeName);
                if (property != null && property.CanWrite)
                {
                    object value = ConvertToCustomType(request.pendingValue, property.PropertyType);
                    property.SetValue(p, value, null);
                    logService.CreateLogAsync("PatientId:" + request.userId + ";Attribute:" + request.attributeName + ";updated with success;", p.Email.Value);
                }
                else
                {
                    return false;
                }
            }

            await unitOfWork.CommitAsync();
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

        public async Task SignUpNewPatientIamAsync(string name, string phoneNumber, string email, string patientEmail, string password)
        {
            var result = patientRepository.PatientExists(patientEmail);

            if (!Regex.IsMatch(phoneNumber, @"^\+\d{1,3}\d{9,15}$"))
            {
                throw new ArgumentException("Phone number is invalid");
            }

            Console.WriteLine("Patient Exists: " + result);

            if (!result)
            {
                throw new InvalidOperationException("Patient Does Not Exist");
            }

            await authService.RegisterNewPatientAsync(name, phoneNumber, email, patientEmail, password);

            Console.WriteLine("Patient Registered in IAM System");

            await unitOfWork.CommitAsync();

            await logService.CreateLogAsync("New patient registered in the IAM system; PatientEmail:" + email, email);
        }
    }
}