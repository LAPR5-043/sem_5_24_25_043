using Domain.LogAggregate;
using Microsoft.AspNetCore.Mvc;
using src.Domain.Shared;

public class LogService : ILogService
{
    private  readonly IUnitOfWork unitOfWork;
    private readonly ILogRepository _logRepository;
    public LogService(ILogRepository logRepository, IUnitOfWork unitOfWork)
    {
        _logRepository = logRepository;
        this.unitOfWork = unitOfWork;
    }


    public async Task<OkObjectResult> getAllLogsAsync()
    {
        var logs = await _logRepository.GetAllAsync();
        return new OkObjectResult(logs);
    }

    public async Task<bool> CreateLogAsync(string action, string email)
    {
        Log log = new Log();
        log.action = action;
        log.email = email;
        log.timestamp = DateTime.Now;
        
        await _logRepository.AddAsync(log);
        await unitOfWork.CommitAsync();

       
        
        return true;
    }

    public async Task<Log> getLogAsync(long id)
    {
        return await _logRepository.GetByIdAsync(new LongId(id));
    }
}