using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure;
using src.Infrastructure.Shared;
using src.Domain.OperationTypeAggregate;
using src.Domain.Shared;

namespace src.Infrastructure.OperationTypes
{
    public class OperationTypeRepository : IOperationTypeRepository
    {
        private readonly DDDSample1DbContext _context;

        public OperationTypeRepository(DDDSample1DbContext context)
        {
            _context = context;
        }

        public async Task<OperationType> GetByIdAsync(long id)
        {
            return await _context.Set<OperationType>().FindAsync(id);
        }

        public async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await _context.Set<OperationType>().ToListAsync();
        }

        public async Task AddAsync(OperationType operationType)
        {
            await _context.Set<OperationType>().AddAsync(operationType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OperationType operationType)
        {
            _context.Set<OperationType>().Update(operationType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<OperationType>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        Task<List<OperationType>> IRepository<OperationType, OperationTypeID>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<OperationType>> GetByIdsAsync(List<long> ids)
        {
            throw new NotImplementedException();
        }

        Task<OperationType> IRepository<OperationType, OperationTypeID>.AddAsync(OperationType obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(OperationType obj)
        {
            throw new NotImplementedException();
        }

        public Task<OperationType> GetByIdAsync(OperationTypeID id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OperationType>> GetByIdsAsync(List<OperationTypeID> ids)
        {
            throw new NotImplementedException();
        }


    }
}