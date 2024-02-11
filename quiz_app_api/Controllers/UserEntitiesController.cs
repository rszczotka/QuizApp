using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quiz_app_api.Data;
using quiz_app_api.Data.Entities;

namespace quiz_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEntitiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserEntitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserEntities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEntity>>> GetUserEntities()
        {
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
