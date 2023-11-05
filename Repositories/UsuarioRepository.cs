using System.Data.SQLite;
using EspacioTareas;
namespace EspacioRepositorios
{    
    public class UsuarioRepository : IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";
        public int CrearUsuario(Usuario user)
        {
            int filasAfectadas;
            var query = $"INSERT INTO Usuario (nombre) VALUES (@user)";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {

                connection.Open();
                var command = new SQLiteCommand(query, connection);

                command.Parameters.Add(new SQLiteParameter("@name", user.Nombre));
                filasAfectadas = command.ExecuteNonQuery();

                connection.Close();
            }
            return filasAfectadas;
        }
        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string queryString = "SELECT id, nombre FROM kanban.Usuario;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var usuario = new Usuario();
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuario.Nombre = reader["nombre"].ToString();
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
            return usuarios;
        }
        public int ModificarUsuario(int id, Usuario user)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE directors SET nombre = '{user.Nombre}' WHERE id = '{id}';";
            connection.Open();
            int filasAfectadas = command.ExecuteNonQuery();
            connection.Close();
            return filasAfectadas;
        }

        public Usuario GetUsuarioById(int idUsuario)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            var usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM directors WHERE id = @idUsuario";
            command.Parameters.Add(new SQLiteParameter("@idusuario", idUsuario));
            connection.Open();
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Nombre = reader["nombre"].ToString();
                }
            }
            connection.Close();

            return usuario;
        }
        public int EliminarUsuario(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM directors WHERE id = '{id}';";
            connection.Open();
            int filasAfectadas = command.ExecuteNonQuery();
            connection.Close();
            return filasAfectadas;
        }
    }
}