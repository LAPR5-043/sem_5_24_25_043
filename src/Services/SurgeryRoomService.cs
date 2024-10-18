using System;
using Domain.SurgeryRoomAggregate;
using src.Domain.Shared;
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
    }
}