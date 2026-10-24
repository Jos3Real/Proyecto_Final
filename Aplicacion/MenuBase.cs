using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

/// <summary>Elementos comunes a todos los menús: encabezado CRUD y control de errores.</summary>
public static class MenuBase
{
    public static int OpcionCrud(string titulo)
    {
        Console.Clear();
        Console.WriteLine("====================================================");
        Console.WriteLine($"  {titulo}");
        Console.WriteLine("====================================================");
        Console.WriteLine("1. Listar");
        Console.WriteLine("2. Agregar");
        Console.WriteLine("3. Actualizar");
        Console.WriteLine("4. Eliminar");
        Console.WriteLine("0. Volver");
        Console.WriteLine("====================================================");
        return Consola.Entero("Seleccione una opción: ");
    }

    // Ejecuta una acción mostrando los errores de forma amigable y luego espera ENTER.
    public static void EjecutarAccion(Action accion)
    {
        Console.WriteLine();
        try
        {
            accion();
        }
        catch (Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No se pudo completar la operación: {error.Message}");
            Console.ResetColor();
        }
        Consola.Pausa();
    }
}
