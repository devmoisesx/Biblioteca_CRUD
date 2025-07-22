using BibliotecaAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<SQLiteDbConfig>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var sqliteDbConfig = services.GetRequiredService<SQLiteDbConfig>();
        sqliteDbConfig.InitializeDatabase();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro crítico ao inicializar o banco de dados: {ex.Message}");
        throw;
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
