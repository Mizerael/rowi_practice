using rowi_practice.Application;

var builder = ApiBuilder.CreateBuilder(args, "Default");
ApiBuilder.AddAuthorization(builder);

var app = ApiApp.CreateApp(builder);
ApiApp.UseAuthorization(app);
ApiApp.SetThrustedDomain(app, new string[] {"http://localhost:5173", "http://localhost:4173"});
ApiApp.UseMigrations(app);

app.Run();