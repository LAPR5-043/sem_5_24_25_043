using Domain.SurgeryRoomAggregate;
using Microsoft.EntityFrameworkCore;
using src.Domain.SurgeryRoomAggregate;
using src.Infrastructure.Shared;
using src.Models;
using AppContext = src.Models.AppContext;


namespace src.Infrastructure.Repositories
{
    public class SurgeryRoomRepository : BaseRepository<SurgeryRoom, RoomId>, ISurgeryRoomRepository
    {
        private readonly AppContext context;

        public SurgeryRoomRepository(AppContext context) : base(context.SurgeryRooms)
        {
            this.context = context;
        }
    }
}