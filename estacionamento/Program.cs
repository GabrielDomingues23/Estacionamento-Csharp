using EstacionamentoAPI.Data;
using EstacionamentoAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=estacionamento.db"));

builder.Services.AddScoped<IEstadiaRepository, EstadiaRepository>();
builder.Services.AddScoped<ICarroRepository, CarroRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();