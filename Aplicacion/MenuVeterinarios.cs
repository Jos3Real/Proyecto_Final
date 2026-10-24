using static VeterinariaV2.Aplicacion.MenuBase;
using VeterinariaV2.Dominio;
using VeterinariaV2.Persistencia;
using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

public static class MenuVeterinarios
{
    public static void Ejecutar()
    {
        int op;
        do
        {
            op = OpcionCrud("VETERINARIOS");
            switch (op)
            {
                case 1: EjecutarAccion(Listar); break;
                case 2: EjecutarAccion(Agregar); break;
                case 3: EjecutarAccion(Actualizar); break;
                case 4: EjecutarAccion(Eliminar); break;
            }
        } while (op != 0);
    }

    public static void Listar()
    {
        var lista = VeterinarioRepositorio.ObtenerTodos();
        if (lista.Count == 0) Console.WriteLine("No hay veterinarios registrados.");
        foreach (var v in lista) Console.WriteLine(v);
    }

    // Muestra la lista y pide un ID; devuelve el veterinario elegido.
    public static Veterinario Elegir()
    {
        Listar();
        int id = Consola.Entero("ID del veterinario: ");
        return VeterinarioRepositorio.ObtenerPorId(id) ?? throw new Exception("No existe un veterinario con ese ID.");
    }

    private static void Agregar()
    {
        var v = new Veterinario
        {
            Nombre = Consola.Texto("Nombre: "),
            Apellido = Consola.Texto("Apellido: "),
            Especialidad = Consola.Texto("Especialidad: "),
            NumeroColegiado = Consola.Texto("Número de colegiado: ")
        };
        VeterinarioRepositorio.Insertar(v);
        Console.WriteLine("Veterinario registrado correctamente.");
    }

    private static void Actualizar()
    {
        var v = Elegir();
        Console.WriteLine("(Presione ENTER para conservar el dato actual)");
        v.Nombre = Consola.Texto("Nombre: ", v.Nombre);
        v.Apellido = Consola.Texto("Apellido: ", v.Apellido);
        v.Especialidad = Consola.Texto("Especialidad: ", v.Especialidad);
        v.NumeroColegiado = Consola.Texto("Número de colegiado: ", v.NumeroColegiado);
        VeterinarioRepositorio.Actualizar(v);
        Console.WriteLine("Veterinario actualizado correctamente.");
    }

    private static void Eliminar()
    {
        var v = Elegir();
        if (!Consola.Confirmar("¿Desea eliminar este veterinario?"))
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }
        VeterinarioRepositorio.Eliminar(v.Id);
        Console.WriteLine("Veterinario eliminado.");
    }
}
