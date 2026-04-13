using EstacionamentoAPI.Data;
using EstacionamentoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EstacionamentoAPI.Repositories;

public class EstadiaRepository
{
    private readonly AppDbContext _context;

    public EstadiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Estadia> Listar()
    {
        return _context.Estadias
            .Include(e => e.Carro)
            .Include(e => e.Vaga)
            .ToList();
    }

    public Estadia? BuscarPorId(int id)
    {
        return _context.Estadias
            .Include(e => e.Carro)
            .Include(e => e.Vaga)
            .FirstOrDefault(e => e.Id == id);
    }

    public void Criar(Estadia estadia)
    {
        _context.Estadias.Add(estadia);
        _context.SaveChanges();
    }

    public void Atualizar(Estadia estadia)
    {
        _context.Estadias.Update(estadia);
        _context.SaveChanges();
    }

    public void Deletar(int id)
    {
        var estadia = _context.Estadias.Find(id);
        if (estadia != null)
        {
            _context.Estadias.Remove(estadia);
            _context.SaveChanges();
        }
    }
}