#define Primary
#if Primary
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZadanieApi.Models;

#region ZadanieController
namespace ZadanieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZadanieController : ControllerBase
    {
        private readonly ZadanieContext _context;
        #endregion

        public ZadanieController(ZadanieContext context)
        {
            _context = context;

            if (_context.Zadanies.Count() == 0)
            {
                // Create a new Zadanie if collection is empty,
                // which means you can't delete all Zadanies.
                _context.Zadanies.Add(new Zadanie { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        #region snippet_GetAll
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zadanie>>> GetZadanies()
        {
            return await _context.Zadanies.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zadanie>> GetZadanie(long id)
        {
            var Zadanie = await _context.Zadanies.FindAsync(id);

            if (Zadanie == null)
            {
                return NotFound();
            }

            return Zadanie;
        }
        #endregion
        #endregion

        #region snippet_Create
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<Zadanie>> PostZadanie(Zadanie item)
        {
            _context.Zadanies.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetZadanie), new { id = item.Id }, item);
        }
        #endregion

        #region snippet_Update
        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZadanie(long id, Zadanie item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZadanie(long id)
        {
            var Zadanie = await _context.Zadanies.FindAsync(id);

            if (Zadanie == null)
            {
                return NotFound();
            }

            _context.Zadanies.Remove(Zadanie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
#endif