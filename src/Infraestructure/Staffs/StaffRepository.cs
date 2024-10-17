using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.Infrastructure;
using src.Infrastructure.Shared;
using src.Domain.Shared;
using AppContext = src.Models.AppContext;

namespace src.Infrastructure.OperationTypes
{
    public class StaffRepository(AppContext context) : BaseRepository<Staff, LicenseNumber>(context.Set<Staff>()), IRepository<Staff, LicenseNumber>
    {
        private readonly AppContext _context = context;

        public async Task<Staff> GetByIdAsync(string id)
        {
            var result = await _context.Set<Staff>().FindAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"Specialization with id {id} not found.");
            }
            return result;
        }

        public new async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Set<Staff>().ToListAsync();
        }

        public new async Task AddAsync(Staff staff)
        {
            await _context.Set<Staff>().AddAsync(  staff);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            _context.Set<Staff>().Update(staff);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<Staff>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async void SaveChangesAsync(){
            await _context.SaveChangesAsync();
        }

        Task<List<Staff>> IRepository<Staff, LicenseNumber>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public bool StaffExists(string id)
        {
        return _context.Staffs.Any(e => e.License() == id);
        }

        public new void Remove(Staff obj)
        {
            _context.Set<Staff>().Remove(obj);
            SaveChangesAsync();
        }


    }
}