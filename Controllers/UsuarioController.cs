using Microsoft.AspNetCore.Mvc;
using EspacioTareas;
namespace tp9.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
    }
    [HttpPost("api/usuario/{usuario}")]
    public ActionResult<Usuario> AgregarUsuario(Usuario user)
    {
        return BadRequest();
    }
    [HttpGet("api/usuario")]
    public ActionResult<List<Usuario>> GetUsuarios()
    {
        return BadRequest();
    }

    [HttpGet("api/usuario/{idUsuario}")]
    public ActionResult<Usuario> GetUsuarioById(int idUsuario)
    {
        return BadRequest();
    }

    [HttpPut("api/usuario/{idUsuario}/nombre/{user}")]
    public ActionResult<Usuario> ModificarUsuario(int idUsuario, Usuario user)
    {
        return BadRequest();
    }
}
