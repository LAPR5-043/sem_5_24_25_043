using System;
using Domain.SpecializationAggregate;
using src.Domain.Shared;
using src.Services.IServices;

namespace src.Services.Services
{
    public class SensitiveDataService : ISensitiveDataService
    {
        private List<string> sensitiveData = new List<string>();

        public SensitiveDataService()
        {
            loadSensitiveData();
        }

        public bool isSensitive(string propertyValue)
        {
            if (sensitiveData.Contains(propertyValue.ToLower()))
            {
                return true;
            }
            return false;
        }

        public  void loadSensitiveData()
        {
            string filePath =   "sensitiveAttributes.csv";


            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var values = line.ToLower().Split(';'); 
                    sensitiveData.AddRange(values);
                }
            }
            else
            {
               throw new Exception("File not found");
            }

        }
    }
}