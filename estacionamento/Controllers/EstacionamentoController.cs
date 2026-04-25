using Microsoft.AspNetCore.Mvc;
using EstacionamentoAPI.Models;
using EstacionamentoAPI.Repositories;

namespace EstacionamentoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstacionamentoController : ControllerBase {
    private readonly ICarroRepository _repository;
    private readonly IVagaRepository _vagaRepository; // 1. Declare o campo
    private const decimal PrecoHora = 12.50m;

    // 2. Adicione ao construtor
    public EstacionamentoController(ICarroRepository repository, IVagaRepository vagaRepository) {
        _repository = repository;
        _vagaRepository = vagaRepository;
    }

    [HttpGet("vagas")]
    public async Task<IActionResult> GetVagas() => Ok(await _repository.ListarTodasVagas());

    [HttpPost("vagas")]
    public async Task<IActionResult> CriarVaga([FromBody] Vaga novaVaga)
    {
        if (string.IsNullOrEmpty(novaVaga.NumeroVaga)) return BadRequest("Número da vaga é obrigatório.");

        // 3. Use a instância injetada (_vagaRepository)
        await _vagaRepository.Adicionar(novaVaga);
        return Ok(new { Mensagem = "Vaga criada com sucesso!", Vaga = novaVaga });
    }
    
    [HttpGet("vagas/{id}")]
    public async Task<IActionResult> GetVagaPorId(int id)
    {
        var vaga = await _repository.ObterVagaPorId(id);

        if (vaga == null)
            return NotFound("Vaga não encontrada");

        return Ok(vaga);
    }

    [HttpPost("entrada/{vagaId}")] 
    public async Task<IActionResult> EntradaCarro(int vagaId, [FromBody] Carro novoCarro) {
        var vaga = await _repository.ObterVagaPorId(vagaId);
        
        if (vaga == null) return NotFound("Vaga inexistente.");
        if (vaga.Status == StatusEstadia.Ocupada) return BadRequest("Esta vaga já possui um carro.");

        vaga.Carro = novoCarro;
        vaga.Status = StatusEstadia.Ocupada;
        vaga.HoraEntrada = DateTime.Now;

        await _repository.AtualizarVaga(vaga!);
        return Ok($"Carro {novoCarro.Placa} estacionado com sucesso.");
    }

    [HttpPut("saida/{vagaId}")] // PUT (Atualizar e Calcular) [cite: 44, 56]
    public async Task<IActionResult> SaidaCarro(int vagaId)
    {
        var vaga = await _repository.ObterVagaPorId(vagaId);

        if (vaga == null || vaga.Status != StatusEstadia.Ocupada)
            return BadRequest("Vaga está vazia ou não existe.");

        // Lógica de Cálculo de Negócio (Requisito 56)
        var permanencia = DateTime.Now - vaga.HoraEntrada!.Value;
        var totalHoras = Math.Ceiling(permanencia.TotalHours);
        var valorPagar = (decimal)totalHoras * PrecoHora;

        vaga.Status = StatusEstadia.Livre;
        vaga.Carro = null;
        vaga.HoraEntrada = null;

        await _repository.AtualizarVaga(vaga!);

        return Ok(new
        {
            Mensagem = "Saída processada",
            HorasEstacionado = totalHoras,
            ValorTotal = valorPagar.ToString("C")
        });
    }
    
    [HttpDelete("vagas/{id}")]
    public async Task<IActionResult> DeleteVaga(int id)
    {
        var vaga = await _repository.ObterVagaPorId(id);

        if (vaga == null)
            return NotFound("Vaga não encontrada");

        if (vaga.Status == StatusEstadia.Ocupada)
            return BadRequest("Não é possível deletar uma vaga ocupada");

        await _vagaRepository.Deletar(id);
        return NoContent();
    }
}