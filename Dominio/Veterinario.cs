namespace VeterinariaV2.Dominio;

/// <summary>Profesional que atiende a las mascotas.</summary>
public class Veterinario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Especialidad { get; set; } = "";
    public string NumeroColegiado { get; set; } = "";

    public override string ToString() =>
        $"[{Id}] Dr. {Nombre} {Apellido} | {Especialidad} | Colegiado: {NumeroColegiado}";
}
