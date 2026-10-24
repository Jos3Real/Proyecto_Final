namespace VeterinariaV2.Dominio;

/// <summary>Dueño de una o más mascotas.</summary>
public class Propietario
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Correo { get; set; } = "";

    public override string ToString() =>
        $"[{Id}] {Nombre} {Apellido} | Tel: {Telefono} | Correo: {Correo}";
}
