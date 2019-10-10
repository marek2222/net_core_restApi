using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZadanieApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ZadanieApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ZadanieController : ControllerBase
  {
      public readonly ZadanieContext _context;

    public ZadanieController(ZadanieContext context)     {
        _context = context;
        if (_context.Zadania.Count() == 0)
        {
                _context.Zadania.Add(new Zadanie {Nazwa = "Rzecz"});
                _context.SaveChanges();
        }
     }

    // These methods implement two GET endpoints:
    // - GET /api/zadanie
    // - GET /api/zadanie/{id}
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Zadanie>>> PobierzZadania(long id){
        return await _context.Zadania.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Zadanie>> PobierzZadanie(long id){
        var zadanie = await _context.Zadania.FindAsync(id);
        if (zadanie == null)
        {
            return NotFound();
        }
        return zadanie;
    }

    // Test the app by calling the two endpoints from a browser. For example:
    // https://localhost5001/api/zadanie
    // https://localhost:5001/api/zadanie/1


    // POST: api/todo
    [HttpPost]
    public async Task <ActionResult<Zadanie>> PostZadanie(Zadanie zadanie){
      _context.Zadania.Add(zadanie);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(PobierzZadanie), new Zadanie{Id = zadanie.Id}, zadanie);
    }


    // PUT: api/Todo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutZadanie(long id, Zadanie zadanie)
    {
        if (id != zadanie.Id)
        {
            return BadRequest();
        }
        _context.Entry(zadanie).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }



    // // POST api/todo
    // [HttpPost("")]
    // public void Post([FromBody] string value) { }

    // // PUT api/todo/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value) { }

    // DELETE api/zadanie/5
    [HttpDelete("{id}")]
    public void DeleteById(int id) { }


    public async Task<IActionResult> DeleteZadanie(long id)
    {
        var zadanie = await _context.Zadania.FindAsync(id);

        if (zadanie == null)
        {
            return NotFound();
        }

        _context.Zadania.Remove(zadanie);
        await _context.SaveChangesAsync();

        return NoContent();
    }
  }
}