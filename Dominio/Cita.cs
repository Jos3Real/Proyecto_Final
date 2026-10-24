namespace VeterinariaV2.Dominio;

/// <summary>Cita de una mascota con un veterinario. Estado: Programada, Atendida o Cancelada.</summary>
public class Cita
{
    public int Id { get; set; }
    public int MascotaId { get; set; }
    public int VeterinarioId { get; set; }
    public DateTime FechaHora { get; set; }
    public string Motivo { get; set; } = "";
    public string Estado { get; set; } = "Programada";

    // Datos de consulta (JOIN); no se guardan en la tabla Citas.
    public string NombreMascota { get; set; } = "";
    public string NombreVeterinario { get; set; } = "";

    public override string ToString() =>
        $"[{Id}] {FechaHora:dd/MM/yyyy HH:mm} | Mascota: {NombreMascota} | Vet: {NombreVeterinario} | {Motivo} | {Estado}";
}
