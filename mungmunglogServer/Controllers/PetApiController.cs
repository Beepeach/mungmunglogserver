using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using mungmunglogServer.Data;
using mungmunglogServer.Models;

namespace mungmunglogServer.Controllers
{
    [Route("api/pet")]
    //[Authorize]
    [ApiController]
    public class PetApiController : CommonApiController
    {
        public PetApiController(UserManager<User> userManager,
           SignInManager<User> signInManager,
           ApplicationDbContext context,
           IConfiguration configuration,
           IHostEnvironment environment) : base(userManager, signInManager, context, configuration, environment)
        {

        }

        // GET: api/pet/list/{familyID}
        [HttpGet("list/{familyId}")]
        public async Task<ActionResult<ListResponse<PetDto>>> GetPetList(int familyId)
        {
            var pets = await _context.Pet
                .Where(p => p.FamilyId == familyId)
                .Select(p => new PetDto(p))
                .ToListAsync();

            return new ListResponse<PetDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "PetList 호출 성공",
                List = pets
            };
        }

        // GET: api/pet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleResponse<PetDto>>> GetPet(int id)
        {
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
                Code = Models.StatusCode.Ok,
                Message = "Pet 호출 성공",
                Data = dto
            };
        }

        // PUT: api/pet/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<CommonResponse>> PutPet(int id, PetPutModel model)
        {
            var pet = await _context.Pet.Where(p => p.PetId == id).FirstOrDefaultAsync();

            if (pet == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "Not Found The Pet"
                };
            }

            pet.Name = model.Name;
            pet.Birthday = new DateTime((long)model.Birthday * 10_000_000, DateTimeKind.Utc);
            pet.Breed = model.Breed;
            pet.Gender = model.Gender;
            pet.FileUrl = model.FileUrl;

            try
            {
                await _context.SaveChangesAsync();

                return new CommonResponse
                {
                    Code = Models.StatusCode.Ok,
                    Message = "Success Put Pet"
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.NotFound,
                        Message = "Not Found The Pet"
                    };
                }
                else
                {
                    return new CommonResponse
                    {
                        Code = Models.StatusCode.Unknown,
                        Message = "Unknown Error"
                    };
                }
            }
        }

        // POST: api/pet
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SingleResponse<PetDto>>> PostPet(PetPostModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return Ok(new SingleResponse<PetDto>
                {
                    Code = Models.StatusCode.NotFound,
                    Message = "유저를 찾을 수 없습니다."
                });
            }

            var family = new Family();

            _context.Family.Add(family);
            await _context.SaveChangesAsync();

            var familyMember = new FamilyMember
            {
                IsMaster = true,
                Status = 1,
                UserId = user.Id,
                FamilyId = family.FamilyId
            };

            // 현재는 1계정에 1가지의 Family만 허용이 된다.
            user.FamilyId = family.FamilyId;

            _context.FamilyMember.Add(familyMember);
            await _context.SaveChangesAsync();

            var newPet = new Pet
            {
                Name = model.Name,
                Birthday = new DateTime((long)(model.Birthday * 10_000_000), DateTimeKind.Utc),
                Breed = model.Breed,
                Gender = model.Gender,
                FileUrl = model.FileUrl,
                FamilyId = family.FamilyId
            };

            _context.Pet.Add(newPet);
            await _context.SaveChangesAsync();

            var dto = new PetDto(newPet);

            return Ok( new SingleResponse<PetDto>
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Post",
                Data = dto
            });
        }

        // DELETE: api/pet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommonResponse>> DeletePet(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return new CommonResponse
                {
                    Code = Models.StatusCode.Ok,
                    Message = "Not Found The Pet"
                };
            }

            _context.Pet.Remove(pet);
            await _context.SaveChangesAsync();

            return new CommonResponse
            {
                Code = Models.StatusCode.Ok,
                Message = "Success Delete"
            };
        }

        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.PetId == id);
        }
    }
}
