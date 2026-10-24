using System.Text;
using VeterinariaV2.Aplicacion;
using VeterinariaV2.Persistencia;

namespace VeterinariaV2;

internal static class Program
{
    private static void Main()
    {
        try
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Sistema de Control Clínico Veterinario v2";
            InicializadorBaseDatos.Inicializar();
            MenuPrincipal.Ejecutar();
        }
        catch (Exception error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No fue posible iniciar el sistema: {error.Message}");
            Console.ResetColor();
        }
    }
}
