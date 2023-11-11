using System.Data.SQLite;
using EspacioTareas;
namespace EspacioRepositorios
{
    public class TareaRepository : ITareaRepository
    {
        private readonly string cadenaConexion = "Data Source=DB/kanban.sql;Cache=Shared";

        public Tarea CrearTarea(Tarea tarea)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                try{ 
                    connection.Open();
                    using(var command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado)";
                        // command.Parameters.AddWithValue("@idTarea", tarea.Id);
                        // command.Parameters.Add(new SQLiteParameter("@idTarea", tarea.Id)); ambos son equivalentes
                        // el id_tarea no va ya que se genera automaticamente
                        command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
                        command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                        command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                        command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                        command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                        command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado",tarea.IdUsuarioAsignado));
                        try{ // consultar si es necesario dos bloques try-catch
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex){
                            Console.WriteLine($"Ha ocurrido un error al realizar la consulta: {ex.Message}");
                        }
                    } 
                }
                catch (Exception ex){
                    Console.WriteLine($"Ha ocurrido un error al acceder a la base de datos: {ex.Message}");
                }  
                finally{
                    connection.Close(); 
                }
            }
            return tarea;
        }

        public int EliminarTarea(int idTarea)
        {
            int filasAfectadas = 0;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();

                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM Tarea WHERE id_tarea = @idTarea";
                        command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                        filasAfectadas = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Asegura que se cierre la conexión, independientemente de si hay una excepción o no
                }
            }

            return filasAfectadas;
        }



        public Tarea GetTareaById(int idTarea)
        {
            var tarea = new Tarea();
            using SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            try
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Tarea WHERE id_tarea = @idTarea;";
                command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
            }
            finally
            {
                connection.Close(); // Asegura que se cierre la conexión, independientemente de si hay una excepción o no
            }
            return tarea;
        }
        public List<Tarea> GetAllTareasDeTablero(int idTablero)
        {
            var listaTareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();
                    SQLiteCommand commando = connection.CreateCommand();
                    commando.CommandText = "SELECT id_tarea, nombre, estado, descripcion, color, id_usuario_asignado, id_tablero FROM Tarea WHERE id_tablero = @idTablero;";
                    commando.Parameters.Add(new SQLiteParameter("@idTablero", idTablero));
                    using(SQLiteDataReader reader = commando.ExecuteReader())
                    {
                        while (reader.Read())
                        {   
                            var tarea = new Tarea
                                {
                                    Id = Convert.ToInt32(reader["id_tarea"]),
                                    IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                    IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Descripcion = reader["descripcion"].ToString(),
                                    Color = reader["color"].ToString(),
                                    Estado = (EstadoTarea)Convert.ToInt32(reader["estado"])
                                };
                            listaTareas.Add(tarea);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Asegura que se cierre la conexión, independientemente de si hay una excepción o no
                }
                return listaTareas;
            }
        }

        public List<Tarea> GetAllTareasDeUsuario(int idUsuario)
        {
            var listaTareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                try
                {
                    connection.Open();
                    using (SQLiteCommand commando = connection.CreateCommand()) {
                        commando.CommandText = "SELECT id_tarea, Tarea.nombre as nombre, estado, descripcion, color, id_usuario_asignado, id_tablero FROM Tarea WHERE id_usuario_asignado = @idUsuario;";
                        commando.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                        using(SQLiteDataReader reader = commando.ExecuteReader())
                        {
                            while (reader.Read()) {
                                var tarea = new Tarea
                                {
                                    Id = Convert.ToInt32(reader["id_tarea"]),
                                    IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                    IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Descripcion = reader["descripcion"].ToString(),
                                    Color = reader["color"].ToString(),
                                    Estado = (EstadoTarea)Convert.ToInt32(reader["estado"])
                                };
                                listaTareas.Add(tarea);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Asegura que se cierre la conexión, independientemente de si hay una excepción o no
                }
                return listaTareas;
            }
        }

        public Tarea ModificarTarea(int idTarea, Tarea tarea)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE Tarea SET nombre = @nombre, descripcion = @descripcion, estado = @estado, color = @color, id_tablero = @idTablero WHERE id_tarea = @idTarea;";
                        command.Parameters.Add(new SQLiteParameter("@idTarea",idTarea));
                        command.Parameters.Add(new SQLiteParameter("@nombre",tarea.Nombre));
                        command.Parameters.Add(new SQLiteParameter("@descripcion",tarea.Descripcion));
                        command.Parameters.Add(new SQLiteParameter("@estado",(int)tarea.Estado));
                        command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                        command.Parameters.Add(new SQLiteParameter("@idTablero",tarea.IdTablero));
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex){
                    Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                finally{
                    connection.Close();
                } 
                return tarea;
            }
        }

        public List<Tarea> GetTareasByEstado(EstadoTarea estado)
        {
            var listaTareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
                try
                {
                    connection.Open();
                    using (SQLiteCommand commando = connection.CreateCommand()) {
                        commando.CommandText = "SELECT id_tarea, nombre, estado, descripcion, color, id_usuario_asignado, id_tablero FROM Tarea WHERE estado = @estado;";
                        commando.Parameters.Add(new SQLiteParameter("@estado", (int)estado));
                        using(SQLiteDataReader reader = commando.ExecuteReader())
                        {
                            while (reader.Read()) {
                                var tarea = new Tarea
                                {
                                    Id = Convert.ToInt32(reader["id_tarea"]),
                                    IdTablero = Convert.ToInt32(reader["id_tablero"]),
                                    IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Descripcion = reader["descripcion"].ToString(),
                                    Color = reader["color"].ToString(),
                                    Estado = (EstadoTarea)Convert.ToInt32(reader["estado"])
                                };
                                listaTareas.Add(tarea);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                finally
                {
                    connection.Close(); // Asegura que se cierre la conexión, independientemente de si hay una excepción o no
                }
                return listaTareas;
            }

        }
        public int AsignarUsuarioATarea(int idUsuario, int idTarea)
        {
            int filasAfectadas = 0;
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                try
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE Tarea SET id_usuario_asignado = @idUsuario WHERE id_tarea = @idTarea;";
                        command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                        command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
                        filasAfectadas = command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex){
                    Console.WriteLine($"Ha ocurrido un error: {ex.Message}");
                }
                finally{
                    connection.Close();
                } 
                return filasAfectadas;
            }
        }
    }
}