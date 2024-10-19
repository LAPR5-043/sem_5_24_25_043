using Microsoft.AspNetCore.Mvc;

public interface ILogService
    {
        Task<OkObjectResult> getAllLogsAsync();
        Task<bool> CreateLogAsync(string action, string email);
        Task<Log> getLogAsync(long id);
    }