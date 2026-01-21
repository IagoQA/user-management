using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs.Person;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonResponseDto>>> GetAll()
        {
            var persons = await _context.Persons
                .Select(u => new PersonResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                })
                .ToListAsync();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonResponseDto>> GetById(int id)
        {
            var person = await _context.Persons
                .Where(u => u.Id == id)
                .Select(u => new PersonResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePersonDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = new Person
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            _context.Persons.Add(person);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, new PersonResponseDto
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdatePersonDto dto)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            person.Name = dto.Name;
            person.Email = dto.Email;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            _context.Persons.Remove(person);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
