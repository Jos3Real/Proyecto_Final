using static VeterinariaV2.Aplicacion.MenuBase;
using VeterinariaV2.Dominio;
using VeterinariaV2.Persistencia;
using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

public static class MenuPropietarios
{
    public static void Ejecutar()
    {
        int op;
        do
        {
            op = OpcionCrud("PROPIETARIOS");
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
        var lista = PropietarioRepositorio.ObtenerTodos();
        if (lista.Count == 0) Console.WriteLine("No hay propietarios registrados.");
        foreach (var p in lista) Console.WriteLine(p);
    }

    // Muestra la lista y pide un ID; devuelve el propietario elegido.
    public static Propietario Elegir()
    {
        Listar();
        int id = Consola.Entero("ID del propietario: ");
        return PropietarioRepositorio.ObtenerPorId(id) ?? throw new Exception("No existe un propietario con ese ID.");
    }

    private static void Agregar()
    {
        var p = new Propietario
        {
            Nombre = Consola.Texto("Nombre: "),
            Apellido = Consola.Texto("Apellido: "),
            Telefono = Consola.Texto("Teléfono: "),
            Correo = Consola.Texto("Correo: ")
        };
        PropietarioRepositorio.Insertar(p);
        Console.WriteLine("Propietario registrado correctamente.");
    }

    private static void Actualizar()
    {
        var p = Elegir();
        Console.WriteLine("(Presione ENTER para conservar el dato actual)");
        p.Nombre = Consola.Texto("Nombre: ", p.Nombre);
        p.Apellido = Consola.Texto("Apellido: ", p.Apellido);
        p.Telefono = Consola.Texto("Teléfono: ", p.Telefono);
        p.Correo = Consola.Texto("Correo: ", p.Correo);
        PropietarioRepositorio.Actualizar(p);
        Console.WriteLine("Propietario actualizado correctamente.");
    }

    private static void Eliminar()
    {
        var p = Elegir();
        if (!Consola.Confirmar("Se eliminarán también sus mascotas, citas y consultas. ¿Continuar?"))
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }
        PropietarioRepositorio.Eliminar(p.Id);
        Console.WriteLine("Propietario eliminado.");
    }
}
