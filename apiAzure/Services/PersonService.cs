using apiAzure.Dtos;
using apiAzure.Models;
using apiAzure.Repositories;

namespace apiAzure.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;

        public PersonService(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<PersonReadDto> CreateAsync(PersonCreateDto dto, CancellationToken cancellationToken = default)
        {
            var person = new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Gender = dto.Gender,
                IpAddress = dto.IpAddress,
                HouseAddress = dto.HouseAddress
            };

            var created = await _repository.CreateAsync(person, cancellationToken);
            return MapToReadDto(created);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<(IReadOnlyList<PersonReadDto> items, int totalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 100) pageSize = 10;

            var total = await _repository.CountAsync(cancellationToken);
            var items = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            var mapped = items.Select(MapToReadDto).ToList();
            return (mapped, total);
        }

        public async Task<PersonReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var person = await _repository.GetByIdAsync(id, cancellationToken);
            return person == null ? null : MapToReadDto(person);
        }

        public async Task<bool> UpdateAsync(int id, PersonUpdateDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null)
            {
                return false;
            }

            existing.FirstName = dto.FirstName;
            existing.LastName = dto.LastName;
            existing.Email = dto.Email;
            existing.Gender = dto.Gender;
            existing.IpAddress = dto.IpAddress;
            existing.HouseAddress = dto.HouseAddress;
            return await _repository.UpdateAsync(existing, cancellationToken);
        }

        private static PersonReadDto MapToReadDto(Person person)
        {
            return new PersonReadDto
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Gender = person.Gender,
                IpAddress = person.IpAddress,
                HouseAddress = person.HouseAddress
            };
        }
    }
}


