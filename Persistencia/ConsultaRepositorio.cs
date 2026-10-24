using Microsoft.Data.Sqlite;
using VeterinariaV2.Dominio;

namespace VeterinariaV2.Persistencia;

/// <summary>CRUD de la tabla Consultas.</summary>
public static class ConsultaRepositorio
{
    // READ
    public static List<Consulta> ObtenerTodos()
    {
        var lista = new List<Consulta>();
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
SELECT c.Id, c.MascotaId, c.VeterinarioId, c.Fecha, c.Sintomas, c.Diagnostico,
       c.Tratamiento, c.PesoRegistrado, m.Nombre, v.Nombre || ' ' || v.Apellido
FROM Consultas c
JOIN Mascotas m ON m.Id = c.MascotaId
JOIN Veterinarios v ON v.Id = c.VeterinarioId
ORDER BY c.Fecha DESC;";
        using var lector = comando.ExecuteReader();
        while (lector.Read())
        {
            lista.Add(new Consulta
            {
                Id = lector.GetInt32(0),
                MascotaId = lector.GetInt32(1),
                VeterinarioId = lector.GetInt32(2),
                Fecha = ConexionBD.TextoAFecha(lector.GetString(3)),
                Sintomas = lector.GetString(4),
                Diagnostico = lector.GetString(5),
                Tratamiento = lector.GetString(6),
                PesoRegistrado = lector.GetDouble(7),
                NombreMascota = lector.GetString(8),
                NombreVeterinario = lector.GetString(9)
            });
        }
        return lista;
    }

    public static Consulta ObtenerPorId(int id) =>
        ObtenerTodos().FirstOrDefault(c => c.Id == id);

    // CREATE
    public static void Insertar(Consulta c)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
INSERT INTO Consultas (MascotaId, VeterinarioId, Fecha, Sintomas, Diagnostico, Tratamiento, PesoRegistrado)
VALUES ($mascota, $veterinario, $fecha, $sintomas, $diagnostico, $tratamiento, $peso);";
        comando.Parameters.AddWithValue("$mascota", c.MascotaId);
        comando.Parameters.AddWithValue("$veterinario", c.VeterinarioId);
        comando.Parameters.AddWithValue("$fecha", ConexionBD.FechaATexto(c.Fecha));
        comando.Parameters.AddWithValue("$sintomas", c.Sintomas);
        comando.Parameters.AddWithValue("$diagnostico", c.Diagnostico);
        comando.Parameters.AddWithValue("$tratamiento", c.Tratamiento);
        comando.Parameters.AddWithValue("$peso", c.PesoRegistrado);
        comando.ExecuteNonQuery();
    }

    // UPDATE
    public static bool Actualizar(Consulta c)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = @"
UPDATE Consultas
SET Fecha = $fecha, Sintomas = $sintomas, Diagnostico = $diagnostico,
    Tratamiento = $tratamiento, PesoRegistrado = $peso
WHERE Id = $id;";
        comando.Parameters.AddWithValue("$fecha", ConexionBD.FechaATexto(c.Fecha));
        comando.Parameters.AddWithValue("$sintomas", c.Sintomas);
        comando.Parameters.AddWithValue("$diagnostico", c.Diagnostico);
        comando.Parameters.AddWithValue("$tratamiento", c.Tratamiento);
        comando.Parameters.AddWithValue("$peso", c.PesoRegistrado);
        comando.Parameters.AddWithValue("$id", c.Id);
        return comando.ExecuteNonQuery() > 0;
    }

    // DELETE
    public static bool Eliminar(int id)
    {
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = "DELETE FROM Consultas WHERE Id = $id;";
        comando.Parameters.AddWithValue("$id", id);
        return comando.ExecuteNonQuery() > 0;
    }
}
