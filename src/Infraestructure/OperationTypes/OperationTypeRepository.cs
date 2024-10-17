using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure;
using src.Infrastructure.Shared;
using src.Domain.Shared;
using AppContext = src.Models.AppContext;

namespace src.Infrastructure.OperationTypes
{
    public class OperationTypeRepository(AppContext context) : BaseRepository<OperationType, OperationTypeID>(context.Set<OperationType>()), IOperationTypeRepository
    {
        private readonly AppContext _context = context;

        public async Task<OperationType> GetByIdAsync(long id)
        {
            var result = await _context.Set<OperationType>().FindAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"OperationType with id {id} not found.");
            }
            return result;
        }

        public new async Task<IEnumerable<OperationType>> GetAllAsync()
        {
            return await _context.Set<OperationType>().ToListAsync();
        }

        public new async Task AddAsync(OperationType operationType)
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

        public async void SaveChangesAsync(){
            await _context.SaveChangesAsync();
        }

        Task<List<OperationType>> IRepository<OperationType, OperationTypeID>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool OperationTypeExists(long id)
        {
        return _context.OperationTypes.Any(e => e.operationTypeId() == id);
        }

        public new void Remove(OperationType obj)
        {
            _context.Set<OperationType>().Remove(obj);
            SaveChangesAsync();
        }

        


    }
}