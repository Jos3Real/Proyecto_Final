using System.Reflection;

namespace VeterinariaV2.Persistencia;

/// <summary>Ejecuta el script "Base de Datos/crear_tablas.sql" (incluido como recurso) para crear las tablas.</summary>
public static class InicializadorBaseDatos
{
    public static void Inicializar()
    {
        string script = LeerScript();
        using var conexion = ConexionBD.Abrir();
        using var comando = conexion.CreateCommand();
        comando.CommandText = script;
        comando.ExecuteNonQuery();
    }

    private static string LeerScript()
    {
        var ensamble = Assembly.GetExecutingAssembly();
        using var stream = ensamble.GetManifestResourceStream("crear_tablas.sql")
            ?? throw new InvalidOperationException("No se encontró el script crear_tablas.sql incluido en el proyecto.");
        using var lector = new StreamReader(stream);
        return lector.ReadToEnd();
    }
}
