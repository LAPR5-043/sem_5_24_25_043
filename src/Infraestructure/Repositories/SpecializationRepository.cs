using Microsoft.EntityFrameworkCore;
using src.Infrastructure.Shared;
using src.Models;
using Domain.SpecializationAggregate;
using AppContext = src.Models.AppContext;


namespace src.Infrastructure.Repositories
{
    public class SpecializationRepository : BaseRepository<Specialization, SpecializationName>, ISpecializationRepository
    {
        private readonly AppContext context;

        
        public SpecializationRepository(AppContext context) : base(context.Specializations)
        {
            this.context = context;
        }
    }
}