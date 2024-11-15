using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.OperationTypeAggregate
{
    /// <summary>
    /// Represents a data transfer object for the OperationType entity.
    /// </summary>
    public class OperationTypeDto
    {
        /// <summary>
        /// Gets or sets the name of the operation type.
        /// </summary>
        public string? OperationTypeName { get; set; }
        public string? OperationTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the estimated duration of the operation type.
        /// </summary>
        public string? EstimatedDurationAnesthesia { get; set; }
        /// <summary>
        /// Gets or sets the estimated duration of the operation type.
        /// </summary>
        public string? EstimatedDurationOperation { get; set; }
        public string? EstimatedDurationCleaning { get; set; }

        /// <summary>
        /// Gets or sets the active status of the operation type.
        /// </summary>
        public bool? IsActive { get; set; }


        /// Gets or sets the dictionary of specializations for the operation type.
        /// </summary>
        public Dictionary<string, string>? Specializations { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public OperationTypeDto(){

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTypeDto"/> class with specified details.
        /// </summary>
        [JsonConstructor]
        public OperationTypeDto(string? operationTypeName,string? operationTypeDescription, string? estimatedDurationAnesthesia, string? estimatedDurationOperation,string? estimatedDurationCleaning, bool? isActive, Dictionary<string, string>? specializations)
        {
            OperationTypeName = operationTypeName;
            OperationTypeDescription = operationTypeDescription;
            EstimatedDurationAnesthesia = estimatedDurationAnesthesia;
            EstimatedDurationOperation = estimatedDurationOperation;
            EstimatedDurationCleaning = estimatedDurationCleaning;
            IsActive = isActive;
            Specializations = specializations ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTypeDto"/> class from an <see cref="OperationType"/> entity.
        /// </summary>
        public OperationTypeDto(OperationType operationType)
        {
            OperationTypeName = operationType.operationTypeName?.ToString() ?? string.Empty; 
            OperationTypeDescription = operationType.operationTypeDescription?.ToString() ?? string.Empty;
            EstimatedDurationAnesthesia = operationType.estimatedDuration?.anesthesia.ToString() ?? string.Empty;
            EstimatedDurationOperation = operationType.estimatedDuration?.operation.ToString() ?? string.Empty;
            EstimatedDurationCleaning = operationType.estimatedDuration?.cleaning.ToString() ?? string.Empty;
            IsActive = operationType.isActive;
            Specializations = operationType.specializations?.ToDictionary(s => s.Key, s => s.Value.ToString()) ?? new Dictionary<string, string>();
        }
    }
}