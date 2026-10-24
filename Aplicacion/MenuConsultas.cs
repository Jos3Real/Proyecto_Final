using static VeterinariaV2.Aplicacion.MenuBase;
using VeterinariaV2.Dominio;
using VeterinariaV2.Persistencia;
using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

public static class MenuConsultas
{
    public static void Ejecutar()
    {
        int op;
        do
        {
            op = OpcionCrud("CONSULTAS MÉDICAS");
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
        var lista = ConsultaRepositorio.ObtenerTodos();
        if (lista.Count == 0) Console.WriteLine("No hay consultas registradas.");
        foreach (var c in lista) Console.WriteLine(c);
    }

    // Muestra la lista y pide un ID; devuelve la consulta elegida.
    public static Consulta Elegir()
    {
        Listar();
        int id = Consola.Entero("ID de la consulta: ");
        return ConsultaRepositorio.ObtenerPorId(id) ?? throw new Exception("No existe una consulta con ese ID.");
    }

    private static void Agregar()
    {
        var mascota = MenuMascotas.Elegir();
        var veterinario = MenuVeterinarios.Elegir();
        var c = new Consulta
        {
            MascotaId = mascota.Id,
            VeterinarioId = veterinario.Id,
            Fecha = Consola.Fecha("Fecha de la consulta (dd/MM/yyyy): "),
            Sintomas = Consola.Texto("Síntomas: "),
            Diagnostico = Consola.Texto("Diagnóstico: "),
            Tratamiento = Consola.Texto("Tratamiento indicado: "),
            PesoRegistrado = Consola.Numero("Peso registrado (kg): ")
        };
        ConsultaRepositorio.Insertar(c);
        Console.WriteLine("Consulta registrada correctamente.");
    }

    private static void Actualizar()
    {
        var c = Elegir();
        Console.WriteLine("(Presione ENTER para conservar el dato actual)");
        c.Fecha = Consola.Fecha("Fecha de la consulta (dd/MM/yyyy): ", c.Fecha);
        c.Sintomas = Consola.Texto("Síntomas: ", c.Sintomas);
        c.Diagnostico = Consola.Texto("Diagnóstico: ", c.Diagnostico);
        c.Tratamiento = Consola.Texto("Tratamiento indicado: ", c.Tratamiento);
        c.PesoRegistrado = Consola.Numero("Peso registrado (kg): ", c.PesoRegistrado);
        ConsultaRepositorio.Actualizar(c);
        Console.WriteLine("Consulta actualizada correctamente.");
    }

    private static void Eliminar()
    {
        var c = Elegir();
        ConsultaRepositorio.Eliminar(c.Id);
        Console.WriteLine("Consulta eliminada.");
    }
}
