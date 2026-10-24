using System.Globalization;
using Microsoft.Data.Sqlite;

namespace VeterinariaV2.Persistencia;

/// <summary>Conexión a SQLite. El archivo veterinaria.db se crea en la carpeta de ejecución.</summary>
public static class ConexionBD
{
    private const string RutaArchivo = "veterinaria.db";

    public static SqliteConnection Abrir()
    {
        var cadena = new SqliteConnectionStringBuilder
        {
            DataSource = RutaArchivo,
            ForeignKeys = true
        }.ToString();

        var conexion = new SqliteConnection(cadena);
        conexion.Open();
        return conexion;
    }

    // Las fechas se guardan como texto "yyyy-MM-dd HH:mm" para leerlas y compararlas fácilmente.
    public static string FechaATexto(DateTime fecha) =>
        fecha.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

    public static DateTime TextoAFecha(string texto) =>
        DateTime.ParseExact(texto, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
}
