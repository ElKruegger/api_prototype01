using Microsoft.EntityFrameworkCore;
using apiAzure.Data;
using apiAzure.Models;

namespace apiAzure.Repositories
{
    public class EfPersonRepository : IPersonRepository
    {
        private readonly ApiDbContext _db;

        public EfPersonRepository(ApiDbContext db)
        {
            _db = db;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _db.People.CountAsync(cancellationToken);
        }

        public async Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default)
        {
            _db.People.Add(person);
            await _db.SaveChangesAsync(cancellationToken);
            return person;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _db.People.FindAsync([id], cancellationToken);
            if (entity == null)
            {
                return false;
            }
            _db.People.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<IReadOnlyList<Person>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _db.People.AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip(Math.Max(0, (pageNumber - 1) * pageSize))
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.People.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default)
        {
            _db.People.Update(person);
            var changes = await _db.SaveChangesAsync(cancellationToken);
            return changes > 0;
        }
    }
}


