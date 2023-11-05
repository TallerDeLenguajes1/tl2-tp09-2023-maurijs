using EspacioTareas;
namespace EspacioRepositorios
{
    public interface ITareaRepository
    {
        int CrearTarea(Tarea tarea);
        List<Tarea> GetAllTareasDeUsuario(int idUsuario);
        List<Tarea> GetAllTareasDeTablero(int idTablero);
        Tarea GetTareaById(int id);
        int ModificarTarea(int id, Tarea tarea);
        int EliminarTarea(int idTarea);
        int AsignarUsuarioATarea(int idUsuario, int idTarea);
    }
}