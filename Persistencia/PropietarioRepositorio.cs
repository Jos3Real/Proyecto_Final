using Microsoft.Data.Sqlite;
using VeterinariaV2.Dominio;

namespace VeterinariaV2.Persistencia;

/// <summary>CRUD de la tabla Propietarios.</summary>
public static class PropietarioRepositorio
{
    // READ
    public static List<Propietario> ObtenerTodos()
    {
        var lista = new List<Propietario>();
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT Id, Nombre, Apellido, Telefono, Correo FROM Propietarios ORDER BY Apellido, Nombre;";
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Propietario
            {
                Id = lector.GetInt32(0),
                Nombre = lector.GetString(1),
                Apellido = lector.GetString(2),
                Telefono = lector.GetString(3),
                Correo = lector.GetString(4)
            });
        }
        return lista;
    }

    public static Propietario ObtenerPorId(int id) =>
        ObtenerTodos().FirstOrDefault(p => p.Id == id);

    // CREATE
    public static void Insertar(Propietario p)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
INSERT INTO Propietarios (Nombre, Apellido, Telefono, Correo)
VALUES ($nombre, $apellido, $telefono, $correo);";
        comando.Parameters.AddWithValue("$nombre", p.Nombre);
        comando.Parameters.AddWithValue("$apellido", p.Apellido);
        comando.Parameters.AddWithValue("$telefono", p.Telefono);
        comando.Parameters.AddWithValue("$correo", p.Correo);
        comando.ExecuteNonQuery();
    }

    // UPDATE
    public static bool Actualizar(Propietario p)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
UPDATE Propietarios
SET Nombre = $nombre, Apellido = $apellido, Telefono = $telefono, Correo = $correo
WHERE Id = $id;";
        comando.Parameters.AddWithValue("$nombre", p.Nombre);
        comando.Parameters.AddWithValue("$apellido", p.Apellido);
        comando.Parameters.AddWithValue("$telefono", p.Telefono);
        comando.Parameters.AddWithValue("$correo", p.Correo);
        comando.Parameters.AddWithValue("$id", p.Id);
        return comando.ExecuteNonQuery() > 0;
    }

    // DELETE (en cascada: también se borran sus mascotas, citas y consultas)
    public static bool Eliminar(int id)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "DELETE FROM Propietarios WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);
        return comando.ExecuteNonQuery() > 0;
    }
}
