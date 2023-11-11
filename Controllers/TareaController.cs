using Microsoft.AspNetCore.Mvc;
using EspacioTareas;
using EspacioRepositorios;
using Microsoft.VisualBasic;
namespace tp9.Controllers;

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase
{
    private readonly TareaRepository repository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        repository = new TareaRepository();
    }


    [HttpGet("api/tarea/{idTarea}")]
    public ActionResult<Tarea> GetTareaById(int idTarea)
    {
        var tarea = repository.GetTareaById(idTarea);
        if(tarea == null) return BadRequest();
        return Ok(tarea);
    }

    [HttpPost("api/tarea")]
    public ActionResult<Tarea> AgregarTarea(Tarea tarea)
    {
        var NuevaTarea = repository.CrearTarea(tarea);
        if (NuevaTarea == null) return BadRequest();
        return Ok(NuevaTarea);
    }

    [HttpPut("api/tarea/{idtarea}/nombre/{tarea}")]
    public ActionResult<Tarea> ModificarTarea(int idTarea, Tarea tarea)
    {
        var tareaModificada = repository.ModificarTarea(idTarea, tarea);
        if (tareaModificada == null) return BadRequest();
        return Ok(tareaModificada);
    }

    [HttpDelete("api/tarea/{idTarea}")]
    public ActionResult<bool> EliminarTarea(int idTarea)
    {
        var resultado = repository.EliminarTarea(idTarea);
        if (resultado > 0) return BadRequest();
        return Ok(true);
    }

    [HttpGet("api/tarea/usuario/{idUsuario}")]
    public ActionResult<List<Tarea>> GetTareasDeUsuario(int idUsuario)
    {
        var listaTareas = repository.GetAllTareasDeUsuario(idUsuario);
        if (listaTareas == null) return BadRequest();
        return Ok(listaTareas);
    }
    
    [HttpGet("api/tarea/tablero/{idTablero}")]
    public ActionResult<List<Tarea>> GetTareasDeTablero(int idTablero)
    {
        var listaTareas = repository.GetAllTareasDeTablero(idTablero);
        if (listaTareas == null) return BadRequest();
        return Ok(listaTareas);
    }
    
    [HttpGet("api/tarea/estado/{estado}")]
    public ActionResult<List<Tarea>> GetTareasByEstado(EstadoTarea estado)
    {
        var listaTareas = repository.GetTareasByEstado(estado);
        if (listaTareas == null) return BadRequest();
        return Ok(listaTareas);
    }
}
