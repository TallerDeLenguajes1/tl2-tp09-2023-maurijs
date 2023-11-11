using Microsoft.AspNetCore.Mvc;
using EspacioTareas;
using EspacioRepositorios;
namespace tp9.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly  UsuarioRepository repository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        repository = new UsuarioRepository();
    }

    [HttpPost("api/usuario")]
    public ActionResult<Usuario> AgregarUsuario(Usuario user)
    {
        var usuario = repository.CrearUsuario(user);
        if (usuario != null) return Ok(usuario);
        return BadRequest();
    }

    [HttpGet("api/usuario")]

    public ActionResult<List<Usuario>> GetUsuarios()
    {
        var listaUsuarios = repository.GetAll();
        if (listaUsuarios != null) return Ok(listaUsuarios);
        return BadRequest();
    }

    [HttpGet("api/usuario/{idUsuario}")]
    public ActionResult<Usuario> GetUsuarioById(int idUsuario)
    {
        var usuario = repository.GetUsuarioById(idUsuario);
        if (usuario != null) return Ok(usuario);
        return BadRequest();
    }

    [HttpPut("api/usuario/{idUsuario}/nombre")]
    public ActionResult<Usuario> ModificarUsuario(int idUsuario, Usuario user)
    {
        var usuario = repository.ModificarUsuario(idUsuario, user);
        if (usuario != null) return Ok(usuario);
        return BadRequest();
    }
}
