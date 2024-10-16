using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure;
using src.Infrastructure.Shared;
using src.Domain.Shared;
using AppContext = src.Models.AppContext;

namespace src.Infrastructure.OperationTypes
{
    public class SpecializationRepository(AppContext context) : BaseRepository<Specialization, SpecializationName>(context.Set<Specialization>()), IRepository<Specialization, SpecializationName>
    {
        private readonly AppContext _context = context;

        public async Task<Specialization> GetByIdAsync(string id)
        {
            var result = await _context.Set<Specialization>().FindAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Specialization with id {id} not found.");
            }
            return result;
        }

        public new async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Set<Specialization>().ToListAsync();
        }

        public new async Task AddAsync(Specialization specialization)
        {
            await _context.Set<Specialization>().AddAsync(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialization specialization)
        {
            _context.Set<Specialization>().Update(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Specialization>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async void SaveChangesAsync(){
            await _context.SaveChangesAsync();
        }

        Task<List<Specialization>> IRepository<Specialization, SpecializationName>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool SpecializationExists(string id)
        {
        return _context.Specializations.Any(e => e.Name() == id);
        }

        public new void Remove(Specialization obj)
        {
            _context.Set<Specialization>().Remove(obj);
            SaveChangesAsync();
        }

        


    }
}