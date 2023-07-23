using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

using rowi_practice.Context;
using rowi_practice.Models;

namespace rowi_practice.Application;

public static class ApiBuilder
{
    public static
    void AddAuthorization(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidIssuer = AuthOptions.ISSUER,
                                ValidateAudience = true,
                                ValidAudience = AuthOptions.AUDIENCE,
                                ValidateLifetime = true,
                                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                                ValidateIssuerSigningKey = true
                            };
                        });
    }

    public static
    WebApplicationBuilder CreateBuilder(string[] args, string Connection)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors();
        builder.Services.AddControllers();

        builder.Services.AddDbContext<DataBaseContext>(option =>
                option.UseMySQL(builder.Configuration.GetConnectionString(Connection)));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}