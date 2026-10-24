using Microsoft.Data.Sqlite;
using VeterinariaV2.Dominio;

namespace VeterinariaV2.Persistencia;

/// <summary>CRUD de la tabla Citas.</summary>
public static class CitaRepositorio
{
    // READ
    public static List<Cita> ObtenerTodos()
    {
        var lista = new List<Cita>();
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
SELECT c.Id, c.MascotaId, c.VeterinarioId, c.FechaHora, c.Motivo, c.Estado,
       m.Nombre, v.Nombre || ' ' || v.Apellido
FROM Citas c
JOIN Mascotas m ON m.Id = c.MascotaId
JOIN Veterinarios v ON v.Id = c.VeterinarioId
ORDER BY c.FechaHora;";
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Cita
            {
                Id = lector.GetInt32(0),
                MascotaId = lector.GetInt32(1),
                VeterinarioId = lector.GetInt32(2),
                FechaHora = ConexionBD.TextoAFecha(lector.GetString(3)),
                Motivo = lector.GetString(4),
                Estado = lector.GetString(5),
                NombreMascota = lector.GetString(6),
                NombreVeterinario = lector.GetString(7)
            });
        }
        return lista;
    }

    public static Cita ObtenerPorId(int id) =>
        ObtenerTodos().FirstOrDefault(c => c.Id == id);

    // CREATE
    public static void Insertar(Cita c)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
INSERT INTO Citas (MascotaId, VeterinarioId, FechaHora, Motivo, Estado)
VALUES ($mascota, $veterinario, $fecha, $motivo, $estado);";
        comando.Parameters.AddWithValue("$mascota", c.MascotaId);
        comando.Parameters.AddWithValue("$veterinario", c.VeterinarioId);
        comando.Parameters.AddWithValue("$fecha", ConexionBD.FechaATexto(c.FechaHora));
        comando.Parameters.AddWithValue("$motivo", c.Motivo);
        comando.Parameters.AddWithValue("$estado", c.Estado);
        comando.ExecuteNonQuery();
    }

    // UPDATE
    public static bool Actualizar(Cita c)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
UPDATE Citas
SET FechaHora = $fecha, Motivo = $motivo, Estado = $estado
WHERE Id = $id;";
        comando.Parameters.AddWithValue("$fecha", ConexionBD.FechaATexto(c.FechaHora));
        comando.Parameters.AddWithValue("$motivo", c.Motivo);
        comando.Parameters.AddWithValue("$estado", c.Estado);
        comando.Parameters.AddWithValue("$id", c.Id);
        return comando.ExecuteNonQuery() > 0;
    }

    // DELETE
    public static bool Eliminar(int id)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "DELETE FROM Citas WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);
        return comando.ExecuteNonQuery() > 0;
    }
}
