using EstacionamentoAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadiaController : ControllerBase
{
    private readonly EstadiaRepository _repository;

    public EstadiaController(EstadiaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var estadias = _repository.Listar();
        return Ok(estadias);
    }
}