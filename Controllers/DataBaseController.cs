using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using rowi_practice.Context;
using rowi_practice.Models;

namespace rowi_practice.Controllers;

[Route("/")]
[ApiController]
public class DataBaseController : ControllerBase
{
    private readonly DataBaseContext _context;

    public
    DataBaseController(DataBaseContext context) => _context = context;

    private
    bool ExistingProblemExists(long id) => (_context.ExistingProblems?
                                                    .Any(e => e.Id == id)).GetValueOrDefault();
    
    bool SolutionExists(long id) => (_context.Solutions?
                                             .Any(e => e.Id == id)).GetValueOrDefault();

    [HttpGet("task")]
    public async
    Task<ActionResult<IEnumerable<ExistingProblem>>> GetExistingProblems()
    {
        if (_context.ExistingProblems is null)
            return NotFound();
        
        return await _context.ExistingProblems.ToListAsync();
    }

    [HttpGet("task/{id}")]
    public async
    Task<ActionResult<ExistingProblem>> GetExistingProblem(long id)
    {
        if (_context.ExistingProblems is null)
            return NotFound();

        var existingProblem = await _context.ExistingProblems.FindAsync(id);
        if (existingProblem is null)
            return NotFound();

        return existingProblem;
    }

    [HttpGet("solution")]
    public async
    Task<ActionResult<IEnumerable<Solution>>> GetSolution()
    {
        if(_context.Solutions is null)
            return NotFound();

        return await _context.Solutions.ToListAsync();
    }

    [HttpGet("solution/{id}")]
    public async
    Task<ActionResult<Solution>> GetSolution(long id)
    {
        if(_context.Solutions is null)
            return NotFound();

        var solution = await _context.Solutions.FindAsync(id);
        if (solution is null)
            return NotFound();

        return solution;
    }

    [HttpPut("task/{id}")]
    public async
    Task<IActionResult> PutExistingProblem(long id, ExistingProblem existingProblem)
    {
        if(id != existingProblem.Id)
            return BadRequest();

        _context.Entry(existingProblem).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if(!SolutionExists(id))
                return NotFound();
            throw;
        }

        return Ok();
    }

    [HttpPut("solution/{id}")]
    public async
    Task<IActionResult> PutSolutionProblem(long id, Solution solution)
    {
        if(id != solution.Id)
            return BadRequest();

        _context.Entry(solution).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if(!SolutionExists(id))
                return NotFound();
            throw;
        }

        return Ok();
    }

    [HttpPost("task")]
    public async
    Task<ActionResult<ExistingProblem>> PostExistingProblem(ExistingProblem existingProblem)
    {
        if(_context.ExistingProblems is null)
            return Problem("Entity set 'DataBaseContext.ExistingProblems' is null.");

        _context.ExistingProblems.Add(existingProblem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExistingProblem), new {id = existingProblem.Id}
                                                         , existingProblem);
    }

    [HttpPost("solution")]
    public async
    Task<ActionResult<Solution>> PostSolutionProblem(Solution solution)
    {
        if(_context.Solutions is null)
            return Problem("Entity set 'DataBaseContext.Solutions is null.");

        _context.Solutions.Add(solution);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSolution), new {id = solution.Id}
                                                  , solution);
    }

    [HttpDelete("task/{id}")]
    public async
    Task<IActionResult> DeleteExistingProblem(long id)
    {
        if(_context.ExistingProblems is null)
            return NotFound();

        var existingProblem = await _context.ExistingProblems.FindAsync(id);
        if(existingProblem is null)
            return NotFound();

        _context.ExistingProblems.Remove(existingProblem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("solution/{id}")]
    public async
    Task<IActionResult> DeleteSolution(long id)
    {
        if(_context.ExistingProblems is null)
            return NotFound();
        
        var solution = await _context.Solutions.FindAsync(id);
        if(solution is null)
            return NotFound();

        _context.Solutions.Remove(solution);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}