using System.Data.SQLite;
using EspacioTareas;
namespace EspacioRepositorios
{
    public class TareaRepository : ITareaRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public int CrearTarea(Tarea tarea)
        {
            int filasAfectadas;
            var query = $"INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado",tarea.IdUsuarioAsignado));
                filasAfectadas = command.ExecuteNonQuery();

                connection.Close();
            }
            return filasAfectadas;
        }

        public int EliminarTarea(int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM Tarea WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            connection.Open();
            int filasAfectadas = command.ExecuteNonQuery();
            connection.Close();
            return filasAfectadas;
        }

        public Tarea GetTareaById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var tarea = new Tarea();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Tarea WHERE id = @idUsuario;";
            command.Parameters.Add(new SQLiteParameter("@idusuario", id));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                }
            }
            connection.Close();

            return tarea;
        }
        public List<Tarea> GetAllTareasDeTablero(int idTablero)
        {
            var listaTareas = new List<Tarea>();
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand commando = connection.CreateCommand();
            commando.CommandText = "SELECT id_tarea as id, tarea.nombre as nombre, estado, descripcion, color, id_usuario_asignado, id_tablero FROM Tarea INNER JOIN Tablero USING (id_tablero) WHERE tablero.id_tablero = @idTablero;";
            commando.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
            connection.Open();
            using(SQLiteDataReader reader = commando.ExecuteReader())
            {
                while (reader.Read())
                {   
                    var tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    listaTareas.Add(tarea);
                }
            }
            return listaTareas;
        }

        public List<Tarea> GetAllTareasDeUsuario(int idUsuario)
        {
            var listaTareas = new List<Tarea>();
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand commando = connection.CreateCommand();
            commando.CommandText = "SELECT id_tarea as id, Tarea.nombre as nombre, estado, descripcion, color, id_usuario_asignado, id_tablero FROM Tarea INNER JOIN Usuario ON Tarea.id_usuario_asignado=Usuario.id WHERE Usuario.id = @idUsuario;";
            commando.Parameters.Add(new SQLiteParameter("@idTablero", idUsuario));
            connection.Open();
            using(SQLiteDataReader reader = commando.ExecuteReader())
            {
                while (reader.Read())
                {   
                    var tarea = new Tarea();
                    tarea.Id = Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    listaTareas.Add(tarea);
                }
            }
            return listaTareas;
        }

        public int ModificarTarea(int idTarea, Tarea tarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Tarea SET nombre = @nombre, descripcion = @descripcion, estado = @estado, color = @color, id_tablero = @idTablero, id_usuario_asignado = @idUsuarioAsignado WHERE id = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("idTarea",idTarea));
            command.Parameters.Add(new SQLiteParameter("nombre",tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("descripcion",tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("estado",(int)tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("id_tablero",tarea.IdTablero));
            command.Parameters.Add(new SQLiteParameter("id_usuario_asignado",tarea.IdUsuarioAsignado));
            connection.Open();
            int filasAfectadas = command.ExecuteNonQuery();
            connection.Close();
            return filasAfectadas;
        }
        public int AsignarUsuarioATarea(int idUsuario, int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id_tarea = @idTarea;";
            command.Parameters.Add(new SQLiteParameter("idUsuario", idUsuario));
            command.Parameters.Add(new SQLiteParameter("idTarea", idTarea));
            connection.Open();
            int filasAfectadas = command.ExecuteNonQuery();
            connection.Close();
            return filasAfectadas;
        }
    }
}