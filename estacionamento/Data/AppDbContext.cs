using Microsoft.EntityFrameworkCore;
using EstacionamentoAPI.Models;

namespace EstacionamentoAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Carro> Carros { get; set; }
    public DbSet<Vaga> Vagas { get; set; }
    public DbSet<Estadia> Estadias { get; set; }
}