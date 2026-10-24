using static VeterinariaV2.Aplicacion.MenuBase;
using VeterinariaV2.Dominio;
using VeterinariaV2.Persistencia;
using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

public static class MenuMascotas
{
    public static void Ejecutar()
    {
        int op;
        do
        {
            op = OpcionCrud("MASCOTAS");
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
        var lista = MascotaRepositorio.ObtenerTodos();
        if (lista.Count == 0) Console.WriteLine("No hay mascotas registradas.");
        foreach (var m in lista) Console.WriteLine(m);
    }

    // Muestra la lista y pide un ID; devuelve la mascota elegida.
    public static Mascota Elegir()
    {
        Listar();
        int id = Consola.Entero("ID de la mascota: ");
        return MascotaRepositorio.ObtenerPorId(id) ?? throw new Exception("No existe una mascota con ese ID.");
    }

    private static void Agregar()
    {
        var propietario = MenuPropietarios.Elegir();
        var m = new Mascota
        {
            PropietarioId = propietario.Id,
            Nombre = Consola.Texto("Nombre de la mascota: "),
            Especie = Consola.Texto("Especie: "),
            Raza = Consola.Texto("Raza: "),
            FechaNacimiento = Consola.Fecha("Fecha de nacimiento (dd/MM/yyyy): "),
            Peso = Consola.Numero("Peso (kg): ")
        };
        ValidarFechaNacimiento(m.FechaNacimiento);
        MascotaRepositorio.Insertar(m);
        Console.WriteLine("Mascota registrada correctamente.");
    }

    private static void Actualizar()
    {
        var m = Elegir();
        Console.WriteLine("(Presione ENTER para conservar el dato actual)");
        m.Nombre = Consola.Texto("Nombre: ", m.Nombre);
        m.Especie = Consola.Texto("Especie: ", m.Especie);
        m.Raza = Consola.Texto("Raza: ", m.Raza);
        m.FechaNacimiento = Consola.Fecha("Fecha de nacimiento (dd/MM/yyyy): ", m.FechaNacimiento);
        m.Peso = Consola.Numero("Peso (kg): ", m.Peso);
        ValidarFechaNacimiento(m.FechaNacimiento);
        MascotaRepositorio.Actualizar(m);
        Console.WriteLine("Mascota actualizada correctamente.");
    }

    private static void Eliminar()
    {
        var m = Elegir();
        if (!Consola.Confirmar("Se eliminarán también sus citas y consultas. ¿Continuar?"))
        {
            Console.WriteLine("Operación cancelada.");
            return;
        }
        MascotaRepositorio.Eliminar(m.Id);
        Console.WriteLine("Mascota eliminada.");
    }

    private static void ValidarFechaNacimiento(DateTime fecha)
    {
        if (fecha.Date > DateTime.Today)
            throw new Exception("La fecha de nacimiento no puede ser futura.");
    }
}
