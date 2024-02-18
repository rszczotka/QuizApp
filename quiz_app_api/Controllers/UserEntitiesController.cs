using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserEntitiesController(AppDbContext _context) : ControllerBase
    {

		// GET: api/users/GetAllUsers/{"api_key": "administrator_api_key"}
		[HttpGet]
        [Route("GetAllUsers/{jsonData}")]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllUsers(string jsonData)
        {
            var userEntity = JsonConvert.DeserializeObject<UserEntity>(jsonData);

            if(userEntity == null) return new List<UserEntity>();

            // if returns false it means there's no admin with such API key
            if(!_context.UserEntities
				.Where(x => x.AccountType == 0)
				.Select(x => x.ApiKey == userEntity.ApiKey)
				.FirstOrDefault()) return new List<UserEntity>();
            
            return await _context.UserEntities.ToListAsync();
		}

        // GET: api/UserEntities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEntity>> GetUserEntity(int id)
        {
            var userEntity = await _context.UserEntities.FindAsync(id);

            if (userEntity == null)
            {
                return NotFound();
            }

            return userEntity;
        }

        // PUT: api/UserEntities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEntity(int id, UserEntity userEntity)
        {
            if (id != userEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(userEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEntityExists(id))
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

        // POST: api/UserEntities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserEntity>> PostUserEntity(UserEntity userEntity)
        {
            _context.UserEntities.Add(userEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEntity", new { id = userEntity.Id }, userEntity);
        }

        // DELETE: api/UserEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEntity(int id)
        {
            var userEntity = await _context.UserEntities.FindAsync(id);
            if (userEntity == null)
            {
                return NotFound();
            }

            _context.UserEntities.Remove(userEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEntityExists(int id)
        {
            return _context.UserEntities.Any(e => e.Id == id);
        }
    }
}
