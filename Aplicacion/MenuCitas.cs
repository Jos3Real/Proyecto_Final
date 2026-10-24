using static VeterinariaV2.Aplicacion.MenuBase;
using VeterinariaV2.Dominio;
using VeterinariaV2.Persistencia;
using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

public static class MenuCitas
{
    public static void Ejecutar()
    {
        int op;
        do
        {
            op = OpcionCrud("CITAS");
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
        var lista = CitaRepositorio.ObtenerTodos();
        if (lista.Count == 0) Console.WriteLine("No hay citas registradas.");
        foreach (var c in lista) Console.WriteLine(c);
    }

    // Muestra la lista y pide un ID; devuelve la cita elegida.
    public static Cita Elegir()
    {
        Listar();
        int id = Consola.Entero("ID de la cita: ");
        return CitaRepositorio.ObtenerPorId(id) ?? throw new Exception("No existe una cita con ese ID.");
    }

    private static void Agregar()
    {
        var mascota = MenuMascotas.Elegir();
        var veterinario = MenuVeterinarios.Elegir();
        var fecha = Consola.Fecha("Fecha y hora (dd/MM/yyyy HH:mm): ", null, "dd/MM/yyyy HH:mm");
        if (fecha < DateTime.Now)
            throw new Exception("La cita debe ser en una fecha y hora futura.");
        ValidarHorarioLibre(veterinario.Id, fecha, 0);

        var c = new Cita
        {
            MascotaId = mascota.Id,
            VeterinarioId = veterinario.Id,
            FechaHora = fecha,
            Motivo = Consola.Texto("Motivo: "),
            Estado = "Programada"
        };
        CitaRepositorio.Insertar(c);
        Console.WriteLine("Cita agendada correctamente.");
    }

    private static void Actualizar()
    {
        var c = Elegir();
        Console.WriteLine("(Presione ENTER para conservar el dato actual)");
        var fecha = Consola.Fecha("Fecha y hora (dd/MM/yyyy HH:mm): ", c.FechaHora, "dd/MM/yyyy HH:mm");
        ValidarHorarioLibre(c.VeterinarioId, fecha, c.Id);
        c.FechaHora = fecha;
        c.Motivo = Consola.Texto("Motivo: ", c.Motivo);
        c.Estado = LeerEstado(c.Estado);
        CitaRepositorio.Actualizar(c);
        Console.WriteLine("Cita actualizada correctamente.");
    }

    private static void Eliminar()
    {
        var c = Elegir();
        CitaRepositorio.Eliminar(c.Id);
        Console.WriteLine("Cita eliminada.");
    }

    // Un veterinario no puede tener dos citas programadas a la misma hora.
    private static void ValidarHorarioLibre(int veterinarioId, DateTime fecha, int citaIdActual)
    {
        bool ocupado = CitaRepositorio.ObtenerTodos().Any(c =>
            c.Id != citaIdActual && c.VeterinarioId == veterinarioId &&
            c.FechaHora == fecha && c.Estado == "Programada");
        if (ocupado)
            throw new Exception("El veterinario ya tiene una cita programada en ese horario.");
    }

    private static string LeerEstado(string actual)
    {
        while (true)
        {
            string letra = Consola.Texto("Estado (P=Programada, A=Atendida, C=Cancelada): ", actual.Substring(0, 1)).ToUpper();
            if (letra == "P") return "Programada";
            if (letra == "A") return "Atendida";
            if (letra == "C") return "Cancelada";
            Console.WriteLine("Opción no válida.");
        }
    }
}
