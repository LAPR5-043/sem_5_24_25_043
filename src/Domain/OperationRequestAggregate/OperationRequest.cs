using System;
using sem_5_24_25_043.Domain.OperationRequestAggregate;
using src.Domain.OperationRequestAggregate;
using src.Domain.Shared;

namespace Domain.OperationRequestAggregate
{   
    /// <summary>
    /// Represents an operation request entity in the domain.
    /// </summary>
    public class OperationRequest : Entity<OperationRequestID>, IAggregateRoot
    {   
        /// <summary>
        /// Represents the ID of the operation request.
        /// </summary>
        public OperationRequestID operationRequestID { get; private set; }
        /// <summary>
        /// Represents the ID of the patient.
        /// </summary>
        public int patientID { get; private set; }
        /// <summary>
        /// Represents the ID of the doctor.
        /// </summary>
        public int doctorID { get; private set; }
        /// <summary>
        /// Represents the ID of the operation type.
        /// </summary>
        public int operationTypeID { get; private set; }
        /// <summary>
        /// Represents the deadline date of the operation request.
        /// </summary>
        public DeadlineDate deadlineDate { get; private set; }
        /// <summary>
        /// Represents the priority of the operation request.
        /// </summary>
        public Priority priority { get; private set; }

        /// <summary>
        /// Default constructor required by Entity Framework.
        /// </summary>
        /// <param name="patientID"></param>  
        /// <param name="doctorID"></param>
        /// <param name="operationTypeID"></param>
        /// <param name="dayDeadline"></param>
        /// <param name="monthDeadline"></param>
        /// <param name="yearDeadline"></param>
        /// <param name="priority"></param>
        public OperationRequest(int patientID, int doctorID, int operationTypeID, int dayDeadline, int monthDeadline, int yearDeadline, string priority)
        {
            this.operationRequestID = new OperationRequestID(Guid.NewGuid());
            this.patientID = patientID;
            this.doctorID = doctorID;
            this.operationTypeID = operationTypeID;
            this.deadlineDate = new DeadlineDate(dayDeadline, monthDeadline, yearDeadline);
            this.priority = PriorityExtensions.FromString(priority);
        }
        /// <summary>
        /// Override of the equality operator.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            OperationRequest other = (OperationRequest)obj;
            return operationRequestID.Equals(other.operationRequestID);
        }

        /// <summary>
        /// Override of the GetHashCode method.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return operationRequestID.GetHashCode();
        }


    }
}