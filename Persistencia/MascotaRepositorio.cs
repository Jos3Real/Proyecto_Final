using Microsoft.Data.Sqlite;
using VeterinariaV2.Dominio;

namespace VeterinariaV2.Persistencia;

/// <summary>CRUD de la tabla Mascotas.</summary>
public static class MascotaRepositorio
{
    // READ
    public static List<Mascota> ObtenerTodos()
    {
        var lista = new List<Mascota>();
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
SELECT m.Id, m.PropietarioId, m.Nombre, m.Especie, m.Raza, m.FechaNacimiento, m.Peso,
       p.Nombre || ' ' || p.Apellido
FROM Mascotas m
JOIN Propietarios p ON p.Id = m.PropietarioId
ORDER BY m.Nombre;";
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Mascota
            {
                Id = lector.GetInt32(0),
                PropietarioId = lector.GetInt32(1),
                Nombre = lector.GetString(2),
                Especie = lector.GetString(3),
                Raza = lector.GetString(4),
                FechaNacimiento = ConexionBD.TextoAFecha(lector.GetString(5)),
                Peso = lector.GetDouble(6),
                NombrePropietario = lector.GetString(7)
            });
        }
        return lista;
    }

    public static Mascota ObtenerPorId(int id) =>
        ObtenerTodos().FirstOrDefault(m => m.Id == id);

    // CREATE
    public static void Insertar(Mascota m)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
INSERT INTO Mascotas (PropietarioId, Nombre, Especie, Raza, FechaNacimiento, Peso)
VALUES ($propietario, $nombre, $especie, $raza, $nacimiento, $peso);";
        comando.Parameters.AddWithValue("$propietario", m.PropietarioId);
        comando.Parameters.AddWithValue("$nombre", m.Nombre);
        comando.Parameters.AddWithValue("$especie", m.Especie);
        comando.Parameters.AddWithValue("$raza", m.Raza);
        comando.Parameters.AddWithValue("$nacimiento", ConexionBD.FechaATexto(m.FechaNacimiento));
        comando.Parameters.AddWithValue("$peso", m.Peso);
        comando.ExecuteNonQuery();
    }

    // UPDATE
    public static bool Actualizar(Mascota m)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
UPDATE Mascotas
SET Nombre = $nombre, Especie = $especie, Raza = $raza, FechaNacimiento = $nacimiento, Peso = $peso
WHERE Id = $id;";
        comando.Parameters.AddWithValue("$nombre", m.Nombre);
        comando.Parameters.AddWithValue("$especie", m.Especie);
        comando.Parameters.AddWithValue("$raza", m.Raza);
        comando.Parameters.AddWithValue("$nacimiento", ConexionBD.FechaATexto(m.FechaNacimiento));
        comando.Parameters.AddWithValue("$peso", m.Peso);
        comando.Parameters.AddWithValue("$id", m.Id);
        return comando.ExecuteNonQuery() > 0;
    }

    // DELETE (en cascada: también se borran sus citas y consultas)
    public static bool Eliminar(int id)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "DELETE FROM Mascotas WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);
        return comando.ExecuteNonQuery() > 0;
    }
}
