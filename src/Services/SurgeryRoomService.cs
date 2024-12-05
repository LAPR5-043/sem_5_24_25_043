using System;
using Domain.SurgeryRoomAggregate;
using src.Domain.Shared;
using src.Domain.SurgeryRoomAggregate;
using src.Services.IServices;

namespace src.Services.Services
{
    public class SurgeryRoomService : ISurgeryRoomService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISurgeryRoomRepository surgeryRoomRepository;

        public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomRepository surgeryRoomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.surgeryRoomRepository = surgeryRoomRepository;

        }

        public Task<SurgeryRoomDto> GetSurgeryRoomAsync(string roomID)
        {
            return surgeryRoomRepository.GetByIdAsync(new RoomId(roomID))
                .ContinueWith(surgeryRoom => new SurgeryRoomDto(surgeryRoom.Result));
        }

        public async Task<List<SurgeryRoomDto>> GetSurgeryRoomsAsync()
        {
            List<SurgeryRoomDto> surgeryRoomsDto = new List<SurgeryRoomDto>();
            var surgeryRooms = await surgeryRoomRepository.GetAllAsync();
            foreach (var surgeryRoom in surgeryRooms)
            {
                surgeryRoomsDto.Add(new SurgeryRoomDto(surgeryRoom));
            }

            return surgeryRoomsDto;
        }

        public async Task<SurgeryRoomDto> CreateSurgeryRoomAsync(SurgeryRoomDto surgeryRoomDto)
        {
            var surgeryRoom = new SurgeryRoom { Name = surgeryRoomDto.Name };
            try
            {
                var result = surgeryRoomRepository.AddAsync(surgeryRoom).Result;
                await unitOfWork.CommitAsync();
                return new SurgeryRoomDto(result);

            }
            catch (Exception e)
            {
                throw new Exception("Room Already Exists");
            }


        }

    }
}