using System;

namespace src.Services.IServices
{
    public interface ISensitiveDataService
    {
        bool isSensitive(string propertyValue);
        void loadSensitiveData();
    }
}