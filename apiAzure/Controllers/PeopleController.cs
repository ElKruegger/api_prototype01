using apiAzure.Dtos;
using apiAzure.Services;
using Microsoft.AspNetCore.Mvc;

namespace apiAzure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _service;

        public PeopleController(IPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var (items, totalCount) = await _service.GetAllAsync(pageNumber, pageSize, cancellationToken);
            var result = new
            {
                pageNumber,
                pageSize,
                totalCount,
                items
            };
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonReadDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var person = await _service.GetByIdAsync(id, cancellationToken);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonReadDto>> Create([FromBody] PersonCreateDto dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var created = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonUpdateDto dto, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var updated = await _service.UpdateAsync(id, dto, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var deleted = await _service.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


