using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using rowi_practice.Context;
using rowi_practice.Models;


namespace rowi_practice.Controllers;

[Route("/")]
[ApiController]
public class DataBaseController : ControllerBase
{
    private readonly DataBaseContext _context;

    private readonly List<string> Roles;

    public
    DataBaseController(DataBaseContext context)
    {
        _context = context;
        Roles = new List<string>(){"administrator", "user"};
    }

    private
    bool ExistingProblemExists(int id) => (_context.ExistingProblem?
                                                   .Any(e => e.Id == id)).GetValueOrDefault();
    
    bool SolutionExists(int id) => (_context.Solution?
                                            .Any(e => e.Id == id)).GetValueOrDefault();

    [HttpGet("tasks")]
    public async
    Task<ActionResult<IEnumerable<ExistingProblem>>> GetExistingProblem()
    {
        if (_context.ExistingProblem is null)
            return NotFound();

        return await _context.ExistingProblem.ToListAsync();
    }

    [HttpGet("tasks/{id}")]
    public async
    Task<ActionResult<ExistingProblem>> GetExistingProblem(int id)
    {
        if (_context.ExistingProblem is null)
            return NotFound();

        var existingProblem = await _context.ExistingProblem.FindAsync(id);
        if (existingProblem is null)
            return NotFound();

        return existingProblem;
    }

    [HttpGet("tasks/{id}/solutions")]
    public async
    Task<ActionResult<IEnumerable<Solution>>> GetSolution(int id)
    {
        if(_context.Solution is null)
            return NotFound();

        return await _context.Solution.Where(s => s.Problem_id == id).ToListAsync();
    }

    [HttpGet("tasks/{tId}/solutions/{sId}")]
    public async
    Task<ActionResult<Solution>> GetSolution(int tId, int sId)
    {
        if(_context.Solution is null)
            return NotFound();

        var solution = await _context.Solution.FindAsync(sId);
        if (solution is null || solution.Problem_id != tId)
            return NotFound();

        return solution;
    }

    [HttpPost("task")]
    public async
    Task<ActionResult<ExistingProblem>> PostExistingProblem(ExistingProblem existingProblem)
    {
        if(_context.ExistingProblem is null)
            return Problem("Entity set 'DataBaseContext.ExistingProblem' is null.");

        _context.ExistingProblem.Add(existingProblem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExistingProblem), new {id = existingProblem.Id}
                                                         , existingProblem);
    }

    [HttpPost("tasks/{id}/solution")]
    public async
    Task<ActionResult<Solution>> PostSolutionProblem(int id, Solution solution)
    {
        if(solution.Problem_id != id)
            return BadRequest();

        if(_context.Solution is null)
            return Problem("Entity set 'DataBaseContext.Solution is null.");

        _context.Solution.Add(solution);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSolution), new {tId = id,  sId = solution.Id}
                                                  , solution);
    }

    [HttpPost("/logins")]
    public async
    Task<IResult> login(string? returnUrl, HttpContext context)
    {
            // получаем из формы email и пароль
        var form = context.Request.Form;
        // если email и/или пароль не установлены, посылаем статусный код ошибки 400
        if (!form.ContainsKey("email") || !form.ContainsKey("password"))
            return Results.BadRequest("Email и/или пароль не установлены");
        string email = form["email"];
        string password = form["password"];
    
        // находим пользователя 
        User? person = _context.User.FirstOrDefault(p => p.Email == email && p.PassCode == password);
        // если пользователь не найден, отправляем статусный код 401
        if (person is null) return Results.Unauthorized();
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, Roles[person.Role])
        };
        var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await context.SignInAsync(claimsPrincipal);
        return Results.Redirect(returnUrl ?? "/");
        }
}