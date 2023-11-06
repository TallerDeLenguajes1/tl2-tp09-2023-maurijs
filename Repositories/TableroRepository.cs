using System.Data.SqlClient;
using System.Data.SQLite;
using EspacioTareas;
namespace EspacioRepositorios
{
    public class TableroRepository : ITableroRepository
    {
        private readonly string cadenaConexion = "Data Source=DB/kanban.db;Cache=Shared";

        public Tablero CrearTablero(Tablero T)
        {
            using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                connection.Open(); // va antes de crear el command
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Tablero (nombre, descripcion, id_usuario_propietario) VALUES (@nombre, @descripcion, @idUsuarioPropietario);";
                command.Parameters.Add(new SQLiteParameter("nombre", T.Nombre));
                command.Parameters.Add(new SQLiteParameter("descripcion", T.Descripcion));
                command.Parameters.Add(new SQLiteParameter("idUsuarioPropietario", T.IdUsuarioPropietario));
                command.ExecuteNonQuery();
                connection.Close();
            }
            return T;   
        }

        public int EliminarTablero(int idTablero)
        {
            throw new NotImplementedException();
        }

        public List<Tablero> GetAllTableros()
        {
            throw new NotImplementedException();
        }

        public List<Tablero> GetAllTablerosDeUsuario(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Tablero GetTableroById(int idTablero)
        {
            throw new NotImplementedException();
        }

        public Tablero ModificarTablero(int idTablero, Tablero T)
        {
            throw new NotImplementedException();
        }
    }

}