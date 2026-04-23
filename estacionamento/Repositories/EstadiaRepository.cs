using EstacionamentoAPI.Models;
using EstacionamentoAPI.Data;

namespace EstacionamentoAPI.Repositories;

public interface IEstadiaRepository {
    List<Estadia> Listar();
    Estadia? BuscarPorId(int id);
    void Criar(Estadia estadia);
    void Atualizar(Estadia estadia);
    void Deletar(int id);
}

public class EstadiaRepository : IEstadiaRepository {
    private readonly AppDbContext _context;

    public EstadiaRepository(AppDbContext context) {
        _context = context;
    }

    public List<Estadia> Listar() {
        return _context.Estadias.ToList();
    }

    public Estadia? BuscarPorId(int id) {
        return _context.Estadias.FirstOrDefault(e => e.Id == id);
    }

    public void Criar(Estadia estadia) {
        _context.Estadias.Add(estadia);
        _context.SaveChanges();
    }

    public void Atualizar(Estadia estadia) {
        _context.Estadias.Update(estadia);
        _context.SaveChanges();
    }

    public void Deletar(int id) {
        var estadia = _context.Estadias.FirstOrDefault(e => e.Id == id);
        if (estadia != null) {
            _context.Estadias.Remove(estadia);
            _context.SaveChanges();
        }
    }
}