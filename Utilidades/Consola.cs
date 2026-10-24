using System.Globalization;

namespace VeterinariaV2.Utilidades;

/// <summary>Lectura de datos desde la consola. Si se pasa un valor actual, ENTER lo conserva.</summary>
public static class Consola
{
    public static string Texto(string mensaje, string actual = null)
    {
        while (true)
        {
            Console.Write(actual == null ? mensaje : $"{mensaje}[{actual}] ");
            string texto = (Console.ReadLine() ?? "").Trim();
            if (texto.Length > 0) return texto;
            if (actual != null) return actual;
            Console.WriteLine("Este campo es obligatorio.");
        }
    }

    public static int Entero(string mensaje, int? actual = null)
    {
        while (true)
        {
            Console.Write(actual.HasValue ? $"{mensaje}[{actual.Value}] " : mensaje);
            string texto = (Console.ReadLine() ?? "").Trim();
            if (texto.Length == 0 && actual.HasValue) return actual.Value;
            if (int.TryParse(texto, out int valor)) return valor;
            Console.WriteLine("Ingrese un número entero válido.");
        }
    }

    public static double Numero(string mensaje, double? actual = null)
    {
        while (true)
        {
            Console.Write(actual.HasValue ? $"{mensaje}[{actual.Value}] " : mensaje);
            string texto = (Console.ReadLine() ?? "").Trim();
            if (texto.Length == 0 && actual.HasValue) return actual.Value;
            if (double.TryParse(texto, NumberStyles.Float, CultureInfo.CurrentCulture, out double valor) && valor > 0)
                return valor;
            Console.WriteLine("Ingrese un número mayor que cero.");
        }
    }

    public static DateTime Fecha(string mensaje, DateTime? actual = null, string formato = "dd/MM/yyyy")
    {
        while (true)
        {
            Console.Write(actual.HasValue ? $"{mensaje}[{actual.Value.ToString(formato)}] " : mensaje);
            string texto = (Console.ReadLine() ?? "").Trim();
            if (texto.Length == 0 && actual.HasValue) return actual.Value;
            if (DateTime.TryParseExact(texto, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                return fecha;
            Console.WriteLine($"Formato inválido. Use {formato}, por ejemplo: {DateTime.Today.ToString(formato)}");
        }
    }

    public static bool Confirmar(string mensaje)
    {
        Console.Write($"{mensaje} (s/n): ");
        return (Console.ReadLine() ?? "").Trim().ToLower() == "s";
    }

    public static void Pausa()
    {
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }
}
