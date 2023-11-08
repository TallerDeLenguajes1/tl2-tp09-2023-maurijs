using Microsoft.AspNetCore.Mvc;
using EspacioTareas;
namespace tp9.Controllers;

[ApiController]
[Route("[controller]")]
public class TableroController : ControllerBase
{
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
    }

    [HttpPost("api/tablero/{tablero}")]
    public ActionResult<Tablero> AgregarTablero(Tablero tablero)
    {
        return BadRequest();
    }
    [HttpGet("api/tablero")]
    public ActionResult<List<Tablero>> GetTablero()
    {
        return BadRequest();
    }
}

/* tarea paradigmas
replicarG n e                                   
| 0>= n = []                                   si 0 >= n devuelve la lista vacia
| otherwise = e : (replicar(n-1) e)            el : concatena e con (replicar(n-1) e) que es una lista
*/  

/*
replicarPM 0 e = []
replicarPM n e= if 0>n then [] else e: replicarPM (null) e*/

//it takes two