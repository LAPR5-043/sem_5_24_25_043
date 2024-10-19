using System;
using src.Domain.Shared;

namespace Domain.PatientAggregate
{   
    /// <summary>
    /// Represents a medical record number of a patient
    /// </summary>
    public class MedicalRecordNumber : EntityId
    {   
        /// <summary>
        /// The medical record number
        /// </summary>
        public int medicalRecordNumber { get; }

        public MedicalRecordNumber() : base(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MedicalRecordNumber"/> class
        /// </summary>
        /// <param name="medicalRecordNumber"></param>
        /// <exception cref="ArgumentException"></exception>
        public MedicalRecordNumber(int medicalRecordNumber) : base(medicalRecordNumber)
        {

            this.medicalRecordNumber = medicalRecordNumber;
        }

        /// <summary>
        /// Compares two medical record numbers for equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (MedicalRecordNumber)obj;
            return medicalRecordNumber == other.medicalRecordNumber;
        }
        
        /// <summary>
        /// Returns the hash code for the medical record number
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return medicalRecordNumber.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the medical record number
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return medicalRecordNumber.ToString();
        }

        protected override object createFromString(string text)
        {
            throw new NotImplementedException();
        }

        public override string AsString()
        {
            return medicalRecordNumber.ToString();
        }
    }
}