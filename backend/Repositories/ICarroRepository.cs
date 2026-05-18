using EstacionamentoAPI.Models;
using EstacionamentoAPI.Data;
using Microsoft.EntityFrameworkCore;

// Interface
public interface ICarroRepository {
    Task<IEnumerable<Vaga>> ListarTodasVagas();
    Task<Vaga?> ObterVagaPorId(int id);
    Task AtualizarVaga(Vaga vaga);
}

// Implementação
public class CarroRepository : ICarroRepository {
    private readonly AppDbContext _context;
    public CarroRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Vaga>> ListarTodasVagas() => 
        await _context.Vagas.Include(v => v.Carro).ToListAsync();

    public async Task<Vaga?> ObterVagaPorId(int id) => 
        await _context.Vagas.Include(v => v.Carro).FirstOrDefaultAsync(v => v.Id == id);

    public async Task AtualizarVaga(Vaga vaga)
    {
        _context.Vagas.Update(vaga);
        await _context.SaveChangesAsync();
    }
    
}