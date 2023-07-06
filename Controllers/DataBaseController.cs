using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using rowi_practice.Context;
using rowi_practice.Models;


namespace rowi_practice.Controllers;

[Route("/")]
[ApiController]
public class DataBaseController : ControllerBase
{
    private readonly DataBaseContext _context;

    public static readonly List<string> ApiRoles = new List<string>(){"administrator", "user"};

    public
    DataBaseController(DataBaseContext context)
    {
        _context = context;
    }

    private
    bool ExistingProblemExists(int id) => (_context.ExistingProblem?
                                                   .Any(e => e.Id == id)).GetValueOrDefault();
    
    bool SolutionExists(int id) => (_context.Solution?
                                            .Any(e => e.Id == id)).GetValueOrDefault();
    [Authorize]
    [HttpGet("tasks")]
    public async
    Task<ActionResult<IEnumerable<ExistingProblem>>> GetExistingProblem()
    {
        if (_context.ExistingProblem is null)
            return NotFound();

        return await _context.ExistingProblem.ToListAsync();
    }
    [Authorize]
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
    [Authorize(Roles= "administrator")]
    [HttpGet("tasks/{id}/solutions")]
    public async
    Task<ActionResult<IEnumerable<Solution>>> GetSolution(int id)
    {
        if(_context.Solution is null)
            return NotFound();

        return await _context.Solution.Where(s => s.Problem_id == id).ToListAsync();
    }
    [Authorize(Roles= "administrator")]
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
    [Authorize(Roles= "admininstrator")]
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
    [Authorize(Roles= "user")]
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

    [HttpPost("/login")]
    public async
    Task<ActionResult<ApiAuth>> login(CleientAuth request)
    {
        User? user = _context.User.FirstOrDefault(u => u.LogCode == request.login &&
                                                       u.PassCode == request.pass);
        if (user is null)
            return Unauthorized();
        
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, ApiRoles[user.Role])
        };
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(15)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey()
                                                       , SecurityAlgorithms.HmacSha256)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        var response = new ApiAuth(){token = encodedJwt, user_id = user.Id};
        return Ok(response);
    }
    
    [HttpPost("/register")]
    public async
    Task<ActionResult<ApiAuth>> register(Registration request)
    {
        User? user = _context.User.FirstOrDefault(u => u.LogCode == request.login);
        if (user is not null)
            return BadRequest();

        if(_context.User is null)
            return Problem("Entity set 'DataBaseContext.User' is null.");

        user = new User(){LogCode = request.login, Email = request.email
                                                 , PassCode = request.pass, Role = (int)Role.user};
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(login)
                               , new {request = new CleientAuth(){login = request.login
                                                                  , pass = request.pass}});
    }
}

