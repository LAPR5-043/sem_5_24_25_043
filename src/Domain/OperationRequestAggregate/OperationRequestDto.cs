using System;
using System.ComponentModel.DataAnnotations;

    public class OperationRequestDto
    {
        
        public long RequestId { get; set; }

        public int patientID { get; set; }

        public string operationType { get; set; }

        public string doctorID { get; set; }
        public string priority { get; set; }

        public int day { get; set; }

        public int month { get; set; }

        public int year { get; set; }


    }
