using BibliotecaAPI.Data;
using BibliotecaAPI.Interface;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using BibliotecaAPI.Storages;
using Serilog;

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs.txt")
    .CreateLogger();

log.Information("Inicializando Aplicação.");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;

    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Biblioteca",
            Version = "V1",
            Description = "API para gerenciamento de Biblioteca."
        };
        return Task.CompletedTask;
    });
});

builder.Services.AddControllers();

// Injeção de Dependência, Cria uma única instância para todo o tempo de vida da aplicação
builder.Services.AddSingleton<SQLiteDbConfig>();

builder.Services.AddScoped<StorageClient>();
builder.Services.AddScoped<StorageCatalog>();

// Injeção de Dependência, Cria uma nova instância para cada solicitação HTTP
builder.Services.AddScoped<IServiceGeneric<Client>, ServiceClient>();
builder.Services.AddScoped<ServiceClient>();
builder.Services.AddScoped<IServiceGeneric<Catalog>, ServiceCatalog>();
builder.Services.AddScoped<ServiceCatalog>();

var app = builder.Build();

// Inicializa o Db SQLite
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var sqliteDbConfig = services.GetRequiredService<SQLiteDbConfig>();
        sqliteDbConfig.InitializeDatabase();    // Cria as Tabelas caso não tenha
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro crítico ao inicializar o banco de dados: {ex.Message}");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

log.Information("Aplicação Finalizada.");
log.CloseAndFlush();
