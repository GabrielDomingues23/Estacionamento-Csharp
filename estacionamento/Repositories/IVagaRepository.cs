using EstacionamentoAPI.Models;
using Microsoft.EntityFrameworkCore;
using EstacionamentoAPI.Data;
namespace EstacionamentoAPI.Repositories;

public interface IVagaRepository
{
    Task<IEnumerable<Vaga>> ListarTodas();
    Task<Vaga?> ObterPorId(int id);
    Task Adicionar(Vaga vaga);
    Task Atualizar(Vaga vaga);
}



public class VagaRepository : IVagaRepository
{
    private readonly AppDbContext _context; // Substitua pelo seu contexto real

    public VagaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vaga>> ListarTodas() 
        => await _context.Vagas.ToListAsync();

    public async Task<Vaga?> ObterPorId(int id) 
        => await _context.Vagas.FindAsync(id);

    public async Task Adicionar(Vaga vaga)
    {
        _context.Vagas.Add(vaga);
        await _context.SaveChangesAsync();
    }

    public async Task Atualizar(Vaga vaga)
    {
        _context.Vagas.Update(vaga);
        await _context.SaveChangesAsync();
    }
}