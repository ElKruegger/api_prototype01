using apiAzure.Models;

namespace apiAzure.Repositories
{
    public interface IPersonRepository
    {
        Task<IReadOnlyList<Person>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Person> CreateAsync(Person person, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Person person, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}


