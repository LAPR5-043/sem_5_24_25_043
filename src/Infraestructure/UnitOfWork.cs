using System.Threading.Tasks;
using AppContext = src.Models.AppContext;
using src.Domain.Shared;

namespace src.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;

        public UnitOfWork(AppContext context)
        {
            this._context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }
    }
}