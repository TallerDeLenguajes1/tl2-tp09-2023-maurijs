using EspacioTareas;

namespace EspacioRepositorios
{
    interface ITableroRepository
    {
        Tablero CrearTablero(Tablero T);
        Tablero ModificarTablero(int idTablero, Tablero T);
        Tablero GetTableroById(int idTablero);
        List<Tablero> GetAllTableros();
        List<Tablero >GetAllTablerosDeUsuario(int idUsuario);
        int EliminarTablero(int idTablero);
    }

    /*CrearunrepositoriollamadoTableroRepositoryparagestionartodaslasoperaciones relacionadascontableros.Esterepositoriodebeincluirmétodospara: ● Crearunnuevotablero(devuelveunobjetoTablero) ●Modificaruntableroexistente(recibeunidyunobjetoTablero) ●ObtenerdetallesdeuntableroporsuID.(recibeunidydevuelveunTablero) ● Listartodoslostablerosexistentes(devuelveunlistdetableros) ● Listartodoslostablerosdeunusuarioespecífico.(recibeunIdUsuario,devuelveun listdetableros) ● EliminaruntableroporID*/
    
}