using Microsoft.Data.Sqlite;
using VeterinariaV2.Dominio;

namespace VeterinariaV2.Persistencia;

/// <summary>CRUD de la tabla Veterinarios.</summary>
public static class VeterinarioRepositorio
{
    // READ
    public static List<Veterinario> ObtenerTodos()
    {
        var lista = new List<Veterinario>();
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "SELECT Id, Nombre, Apellido, Especialidad, NumeroColegiado FROM Veterinarios ORDER BY Apellido, Nombre;";
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Veterinario
            {
                Id = lector.GetInt32(0),
                Nombre = lector.GetString(1),
                Apellido = lector.GetString(2),
                Especialidad = lector.GetString(3),
                NumeroColegiado = lector.GetString(4)
            });
        }
        return lista;
    }

    public static Veterinario ObtenerPorId(int id) =>
        ObtenerTodos().FirstOrDefault(v => v.Id == id);

    // CREATE
    public static void Insertar(Veterinario v)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
INSERT INTO Veterinarios (Nombre, Apellido, Especialidad, NumeroColegiado)
VALUES ($nombre, $apellido, $especialidad, $colegiado);";
        comando.Parameters.AddWithValue("$nombre", v.Nombre);
        comando.Parameters.AddWithValue("$apellido", v.Apellido);
        comando.Parameters.AddWithValue("$especialidad", v.Especialidad);
        comando.Parameters.AddWithValue("$colegiado", v.NumeroColegiado);
        try
        {
            comando.ExecuteNonQuery();
        }
        catch (SqliteException error) when (error.SqliteErrorCode == 19) // UNIQUE
        {
            throw new InvalidOperationException("Ya existe un veterinario con ese número de colegiado.");
        }
    }

    // UPDATE
    public static bool Actualizar(Veterinario v)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
UPDATE Veterinarios
SET Nombre = $nombre, Apellido = $apellido, Especialidad = $especialidad, NumeroColegiado = $colegiado
WHERE Id = $id;";
        comando.Parameters.AddWithValue("$nombre", v.Nombre);
        comando.Parameters.AddWithValue("$apellido", v.Apellido);
        comando.Parameters.AddWithValue("$especialidad", v.Especialidad);
        comando.Parameters.AddWithValue("$colegiado", v.NumeroColegiado);
        comando.Parameters.AddWithValue("$id", v.Id);
        try
        {
            return comando.ExecuteNonQuery() > 0;
        }
        catch (SqliteException error) when (error.SqliteErrorCode == 19)
        {
            throw new InvalidOperationException("Ya existe un veterinario con ese número de colegiado.");
        }
    }

    // DELETE (no se permite si tiene citas o consultas)
    public static bool Eliminar(int id)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "DELETE FROM Veterinarios WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);
        try
        {
            return comando.ExecuteNonQuery() > 0;
        }
        catch (SqliteException error) when (error.SqliteErrorCode == 19) // clave foránea
        {
            throw new InvalidOperationException("No se puede eliminar: el veterinario tiene citas o consultas registradas.");
        }
    }
}
