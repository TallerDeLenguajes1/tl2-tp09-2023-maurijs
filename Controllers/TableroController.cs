using Microsoft.AspNetCore.Mvc;
using EspacioTareas;
using EspacioRepositorios;
namespace tp9.Controllers;

[ApiController]
[Route("[controller]")]
public class TableroController : ControllerBase
{
    private readonly ILogger<TableroController> _logger;
    private readonly TableroRepository repository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        repository = new TableroRepository();
    }

    [HttpPost("api/tablero")]
    public ActionResult<Tablero> AgregarTablero(Tablero tablero)
    {
        var nuevo = repository.CrearTablero(tablero);
        if (nuevo != null) return Ok(nuevo);
        return BadRequest();
    }

    [HttpGet("api/tablero")]
    public ActionResult<List<Tablero>> GetTableros()
    {
        var listaTableros = repository.GetAllTableros();
        if (listaTableros != null) return Ok(listaTableros);
        return BadRequest();
    }

    [HttpGet("api/tablero/{idTablero}")]
    public ActionResult<List<Tablero>> GetTableroById(int idTablero)
    {
        var tablero = repository.GetTableroById(idTablero);
        if (tablero != null) return Ok(tablero);
        return BadRequest();
    }
}
