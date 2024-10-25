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

        /// <summary>
        /// Gets or sets the estimated duration of the operation type.
        /// </summary>
        public string? EstimatedDurationHours { get; set; }
        /// <summary>
        /// Gets or sets the estimated duration of the operation type.
        /// </summary>
        public string? EstimatedDurationMinutes { get; set; }

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
        public OperationTypeDto(string? operationTypeName, string? estimatedDurationHours, string? estimatedDurationMinutes, bool? isActive, Dictionary<string, string>? specializations)
        {
            OperationTypeName = operationTypeName;
            EstimatedDurationHours = estimatedDurationHours;
            EstimatedDurationMinutes = estimatedDurationMinutes;
            IsActive = isActive;
            Specializations = specializations ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationTypeDto"/> class from an <see cref="OperationType"/> entity.
        /// </summary>
        public OperationTypeDto(OperationType operationType)
        {
            OperationTypeName = operationType.operationTypeName?.ToString() ?? string.Empty; 
            EstimatedDurationHours = operationType.estimatedDuration?.hours.ToString() ?? string.Empty;
            EstimatedDurationMinutes = operationType.estimatedDuration?.minutes.ToString() ?? string.Empty;
            IsActive = operationType.isActive;
            Specializations = operationType.specializations?.ToDictionary(s => s.Key, s => s.Value.ToString()) ?? new Dictionary<string, string>();
        }
    }
}