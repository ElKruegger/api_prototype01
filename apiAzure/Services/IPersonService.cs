using apiAzure.Dtos;

namespace apiAzure.Services
{
    public interface IPersonService
    {
        Task<(IReadOnlyList<PersonReadDto> items, int totalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<PersonReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PersonReadDto> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, PersonUpdateDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}


