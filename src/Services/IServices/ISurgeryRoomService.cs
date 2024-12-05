using System;
using src.Domain.SurgeryRoomAggregate;

namespace src.Services.IServices
{
    public interface ISurgeryRoomService
    {
        Task<List<SurgeryRoomDto>> GetSurgeryRoomsAsync();
        Task<SurgeryRoomDto> GetSurgeryRoomAsync(string roomID);
        Task<SurgeryRoomDto> CreateSurgeryRoomAsync(SurgeryRoomDto surgeryRoomDto);
    }   
}