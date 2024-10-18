using src.Domain.Shared;
using src.Domain.SurgeryRoomAggregate;

namespace Domain.SurgeryRoomAggregate
{
    public interface ISurgeryRoomRepository : IRepository<SurgeryRoom, RoomNumber>
    {
    }
}