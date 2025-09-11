using apiAzure.Models;

namespace apiAzure.Repositories
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly List<Person> _people = new();
        private int _nextId = 1;
        private readonly object _lock = new();

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                return Task.FromResult(_people.Count);
            }
        }

        public Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                person.Id = _nextId++;
                _people.Add(person);
                return Task.FromResult(person);
            }
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var index = _people.FindIndex(p => p.Id == id);
                if (index == -1)
                {
                    return Task.FromResult(false);
                }
                _people.RemoveAt(index);
                return Task.FromResult(true);
            }
        }

        public Task<IReadOnlyList<Person>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var skip = Math.Max(0, (pageNumber - 1) * pageSize);
                var items = _people
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
                return Task.FromResult((IReadOnlyList<Person>)items);
            }
        }

        public Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var person = _people.FirstOrDefault(p => p.Id == id);
                return Task.FromResult(person);
            }
        }

        public Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var existing = _people.FirstOrDefault(p => p.Id == person.Id);
                if (existing == null)
                {
                    return Task.FromResult(false);
                }
                existing.FirstName = person.FirstName;
                existing.LastName = person.LastName;
                existing.Email = person.Email;
                existing.Gender = person.Gender;
                existing.IpAddress = person.IpAddress;
                existing.HouseAddress = person.HouseAddress;
                return Task.FromResult(true);
            }
        }
    }
}


