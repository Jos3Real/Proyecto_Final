namespace VeterinariaV2.Dominio;

/// <summary>Consulta médica realizada a una mascota.</summary>
public class Consulta
{
    public int Id { get; set; }
    public int MascotaId { get; set; }
    public int VeterinarioId { get; set; }
    public DateTime Fecha { get; set; }
    public string Sintomas { get; set; } = "";
    public string Diagnostico { get; set; } = "";
    public string Tratamiento { get; set; } = "";
    public double PesoRegistrado { get; set; }

    // Datos de consulta (JOIN); no se guardan en la tabla Consultas.
    public string NombreMascota { get; set; } = "";
    public string NombreVeterinario { get; set; } = "";

    public override string ToString() =>
        $"[{Id}] {Fecha:dd/MM/yyyy} | {NombreMascota} | Vet: {NombreVeterinario}\n" +
        $"      Síntomas: {Sintomas} | Diagnóstico: {Diagnostico}\n" +
        $"      Tratamiento: {Tratamiento} | Peso: {PesoRegistrado:0.00} kg";
}
