using Microsoft.AspNetCore.Mvc;
using EspacioTareas;
namespace tp9.Controllers;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase
{
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
    }


    [HttpGet("api/tarea/{idTarea}")]
    public ActionResult<Tarea> GetTareaById(int idTarea)
    {
        return BadRequest();
    }
    
    [HttpGet("api/tarea")]
    public ActionResult<List<Tarea>> GetTareas()
    {
        return BadRequest();
    }

    [HttpPost("api/tarea")]
    public ActionResult<Tarea> AgregarTarea(Tarea tarea)
    {
        return BadRequest();
    }

    [HttpPut("api/tarea/{idtarea}/nombre/{tarea}")]
    public ActionResult<Tarea> ModificarTarea(int idtarea, Tarea tarea)
    {
        return BadRequest();
    }

    [HttpDelete("api/tarea/{idtarea}")]
    public ActionResult<bool> EliminarTarea(int idtarea)
    {
        return BadRequest();
    }
    
    [HttpGet("api/tarea/{estado}")]
    public ActionResult<Tarea> TareasConEseEstado(EstadoTarea estado)
    {
        return BadRequest();
    }

    [HttpGet("api/tarea/usuario/{idUsuario}")]
    public ActionResult<List<Tarea>> GetTareasDeUsuario(int idUsuario)
    {
        return BadRequest();
    }
    
    [HttpGet("api/tarea/tablero/{idTablero}")]
    public ActionResult<List<Tarea>> GetTareasDeTablero(int idTablero)
    {
        return BadRequest();
    }


}
