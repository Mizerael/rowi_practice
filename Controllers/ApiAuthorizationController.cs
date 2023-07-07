using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using rowi_practice.Context;
using rowi_practice.Models;

namespace rowi_practice.Controllers;

[Route("/")]
[ApiController]
public class ApiAuthorizationsController : ControllerBase
{
    private readonly DataBaseContext _context;
    public static readonly List<string> ApiRoles = new List<string>(){"administrator", "user"};
    
    public
    ApiAuthorizationsController(DataBaseContext context) => _context = context;

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
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(30)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey()
                                                       , SecurityAlgorithms.HmacSha256)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        var response = new ApiAuth(){result= encodedJwt, user_id= user.Id};
        return Ok(response);
    }
    
    [HttpPost("/register")]
    public async
    Task<ActionResult<ApiAuth>> register(Registration request)
    {
        User? user = _context.User.FirstOrDefault(u => u.LogCode == request.login ||
                                                       u.Email == request.email);
        if (user is not null)
            return BadRequest();

        if(_context.User is null)
            return Problem("Entity set 'DataBaseContext.User' is null.");

        user = new User(){LogCode = request.login, Email = request.email
                                                 , PassCode = request.pass, Role = (int)Role.user};
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        return Ok(request);
    }
}