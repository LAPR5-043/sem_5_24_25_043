using System;
using src.Domain.SurgeryRoomAggregate;

namespace src.Services.IServices
{
    public interface ISurgeryRoomService
    {
        public Task<List<SurgeryRoomDto>> GetSurgeryRoomsAsync();
        Task<SurgeryRoomDto> GetSurgeryRoomAsync(string roomID);
    }   
}