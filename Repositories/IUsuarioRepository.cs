using EspacioTareas;
namespace EspacioRepositorios
{
    public interface IUsuarioRepository
    {
        int CrearUsuario(Usuario user);
        List<Usuario> GetAll();
        int ModificarUsuario(int id, Usuario user);
        Usuario GetUsuarioById(int id);
        int EliminarUsuario(int id);
    }
}