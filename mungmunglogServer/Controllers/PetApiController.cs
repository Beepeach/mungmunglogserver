using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    [Route("api/pet")]
    //[Authorize]
    [ApiController]
    public class PetApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/pet/list
        [HttpGet("list")]
        public async Task<ActionResult<ListResponse<PetDto>>> GetPetList()
        {
            var pets = await _context.Pet
                .Select(p => new PetDto(p))
                .ToListAsync();

            var petListResponse = new ListResponse<PetDto>()
            {
                Code = Models.StatusCode.Ok,
                Message = "PetList 호출 성공",
                List = pets
            };

            return petListResponse;
        }

        // GET: api/pet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleResponse<PetDto>>> GetPet(int id)
        {
            // include Family를 할것인가?!?
            var pet = await _context.Pet
                .Where(p => p.PetId == id)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                return new SingleResponse<PetDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Pet을 찾을 수 없습니다."
                };
            }

            var dto = new PetDto(pet);

            return new SingleResponse<PetDto>
            {
                Code = Models.StatusCode.NotFound,
                Message = "Pet 호출 성공",
                Data = dto
            };
        }

        // PUT: api/PetApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.PetId)
            {
                return BadRequest();
            }

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PetApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            _context.Pet.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { id = pet.PetId }, pet);
        }

        // DELETE: api/PetApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pet>> DeletePet(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pet.Remove(pet);
            await _context.SaveChangesAsync();

            return pet;
        }

        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.PetId == id);
        }
    }
}
