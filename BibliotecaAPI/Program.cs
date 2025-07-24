using BibliotecaAPI.Data;
using BibliotecaAPI.Interface;
using BibliotecaAPI.Services;
using BibliotecaAPI.Storages;

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

builder.Services.AddSingleton<SQLiteDbConfig>();

builder.Services.AddSingleton<StorageClient>();
builder.Services.AddSingleton<StorageCatalog>();

builder.Services.AddScoped<IServiceClient, ServiceClient>();
builder.Services.AddScoped<ServiceClient>();
builder.Services.AddScoped<IServiceGeneric<Catalog>, ServiceCatalog>();
builder.Services.AddScoped<ServiceCatalog>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
