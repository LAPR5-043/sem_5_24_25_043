namespace sem_5_24_25_043.Models
{
    public class OperationTypeDto
    {
        public long OperationTypeID { get; set; }
        public bool IsActive { get; set; }
        public string OperationTypeName { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}