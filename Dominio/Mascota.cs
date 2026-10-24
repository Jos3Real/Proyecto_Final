namespace VeterinariaV2.Dominio;

/// <summary>Paciente del veterinario.</summary>
public class Mascota
{
    public int Id { get; set; }
    public int PropietarioId { get; set; }
    public string Nombre { get; set; } = "";
    public string Especie { get; set; } = "";
    public string Raza { get; set; } = "";
    public DateTime FechaNacimiento { get; set; }
    public double Peso { get; set; }

    // Dato de consulta (JOIN con Propietarios); no se guarda en la tabla Mascotas.
    public string NombrePropietario { get; set; } = "";

    public override string ToString() =>
        $"[{Id}] {Nombre} ({Especie} - {Raza}) | Peso: {Peso:0.00} kg | Dueño: {NombrePropietario}";
}
