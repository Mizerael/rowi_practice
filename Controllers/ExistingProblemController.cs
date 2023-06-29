using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rowi_practice.Models;

namespace rowi_practice.Controllers
{
    [Route("task")]
    [ApiController]
    public class ExistingProblemController : ControllerBase
    {
        private readonly ExistingProblemContext _context;

        public ExistingProblemController(ExistingProblemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExistingProblem>>> GetExistingProblems()
        {
          if (_context.ExistingProblems == null)
          {
              return NotFound();
          }
            return await _context.ExistingProblems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExistingProblem>> GetExistingProblem(long id)
        {
          if (_context.ExistingProblems == null)
          {
              return NotFound();
          }
            var existingProblem = await _context.ExistingProblems.FindAsync(id);

            if (existingProblem == null)
            {
                return NotFound();
            }

            return existingProblem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistingProblem(long id, ExistingProblem existingProblem)
        {
            if (id != existingProblem.Id)
            {
                return BadRequest();
            }

            _context.Entry(existingProblem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExistingProblemExists(id))
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

        [HttpPost]
        public async Task<ActionResult<ExistingProblem>> PostExistingProblem(ExistingProblem existingProblem)
        {
          if (_context.ExistingProblems == null)
          {
              return Problem("Entity set 'ExistingProblemContext.ExistingProblems'  is null.");
          }
            _context.ExistingProblems.Add(existingProblem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExistingProblem), new { id = existingProblem.Id }, existingProblem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistingProblem(long id)
        {
            if (_context.ExistingProblems == null)
            {
                return NotFound();
            }
            var existingProblem = await _context.ExistingProblems.FindAsync(id);
            if (existingProblem == null)
            {
                return NotFound();
            }

            _context.ExistingProblems.Remove(existingProblem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExistingProblemExists(long id)
        {
            return (_context.ExistingProblems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
