using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace rowi_practice.Models;

public static class AuthOptions
{
    public const string ISSUER = "localhost";
    public const string AUDIENCE = "localhost";
    const string KEY = "{aboba_aboba_aboba_}";
    public static 
    SymmetricSecurityKey GetSymmetricSecurityKey() => 
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}

public class ApiAuth
{
    public string token { get; set; }
    public long user_id { get; set; }
}
public class CleientAuth
{
    public string login { get; set; }
    public string pass { get; set; }
}

public class Registration : CleientAuth
{
    public string email { get; set; }
}