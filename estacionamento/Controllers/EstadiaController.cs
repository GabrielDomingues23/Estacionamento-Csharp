using EstacionamentoAPI.Models;
using EstacionamentoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadiaController : ControllerBase {
    private readonly IEstadiaRepository _repository;
    private const decimal PrecoHora = 15.00m; // Valor para o cálculo [cite: 56]

    public EstadiaController(IEstadiaRepository repository) {
        _repository = repository;
    }

    [HttpGet] // GET (listar todos) [cite: 41]
    public IActionResult Get() => Ok(_repository.Listar());

    [HttpGet("{id}")] // GET por ID [cite: 42]
    public IActionResult GetById(int id) {
        var estadia = _repository.BuscarPorId(id);
        return estadia == null ? NotFound() : Ok(estadia);
    }

    [HttpPost] // POST (criar) [cite: 43]
    public IActionResult Post([FromBody] Estadia estadia) {
        // VALIDAÇÃO: Campos obrigatórios [cite: 57]
        if (estadia.CarroId <= 0 || estadia.VagaId <= 0) 
            return BadRequest("Carro e Vaga são obrigatórios.");

        estadia.DataEntrada = DateTime.Now;
        estadia.Status = StatusEstadia.Ocupada; // Uso do Enum [cite: 53]
        _repository.Criar(estadia);
        return CreatedAtAction(nameof(GetById), new { id = estadia.Id }, estadia);
    }

    [HttpPut("finalizar/{id}")] // PUT (atualizar com CÁLCULO) [cite: 44, 56]
    public IActionResult FinalizarEstadia(int id) {
        var estadia = _repository.BuscarPorId(id);
        if (estadia == null || estadia.Status == StatusEstadia.Livre)
            return BadRequest("Estadia não encontrada ou já finalizada.");

        estadia.DataSaida = DateTime.Now;
        
        // REGRA DE NEGÓCIO: Cálculo real de valor 
        var tempo = estadia.DataSaida.Value - estadia.DataEntrada;
        var horas = Math.Ceiling(tempo.TotalHours); // Arredonda para cima
        if (horas < 1) horas = 1; // Cobrança mínima de 1h

        estadia.ValorCobrado = (decimal)horas * PrecoHora;
        estadia.Status = StatusEstadia.Livre;

        _repository.Atualizar(estadia);
        return Ok(new { 
            Mensagem = "Estadia finalizada", 
            ValorTotal = estadia.ValorCobrado.ToString("C"),
            Horas = horas 
        });
    }

    [HttpDelete("{id}")] // DELETE [cite: 45]
    public IActionResult Delete(int id) {
        _repository.Deletar(id);
        return NoContent();
    }
}