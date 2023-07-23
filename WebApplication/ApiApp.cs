using Microsoft.EntityFrameworkCore;

using rowi_practice.Context;

namespace rowi_practice.Application;

public static class ApiApp
{

    public static
    void UseMigrations(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<DataBaseContext>();
            Console.Write("Waiting for the database configuration to complete");
            while (!context.Database.CanConnect())
                System.Threading.Thread.Sleep(1000);
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }
    }

    public static
    void SetThrustedDomain(WebApplication app, string[] domains)
    {
        app.UseCors(b => b.WithOrigins(domains)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials());
    }
    public static
    void UseAuthorization(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static
    WebApplication CreateApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseHttpsRedirection();

        app.MapControllers();

        return app;
    }
}